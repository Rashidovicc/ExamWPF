using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using examWPF.Service.DTOs.UserDTO;
using examWPF.Service.Interfaces;
using examWPF.Service.Service;
using examWPF.UI.Windows;

namespace examWPF.UI.Pages
{
    public partial class SavePage : Page
    {
        private string _passportImagePath;
        private string _imagePath;
        private MessageViewer _messageViewer;
        public SavePage()
        {
            _messageViewer = new MessageViewer();
            InitializeComponent();
        }

      
        private void ImageUploader_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.PNG,*.JPG;)|*.JPG;*.PNG";
            openFileDialog.InitialDirectory = Environment.GetFolderPath
                (Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                
                _imagePath = openFileDialog.FileName;
                Image.Text = _imagePath.Split('\\').Last();
            }
        }
        
        private async void UserSaveBtn_Click(object sender, RoutedEventArgs e)
        {
           var IsValidId = long.TryParse(NewStudentID.Text, out long result);

            if (IsValidId)
            {
                using IUserService studentService = new UserService();

                var oldStudent = await studentService.GetAsync(result);

                if (oldStudent is null)
                {
                    ErrorResponse.Text = "Student not fount";
                    ErrorResponse.Visibility = Visibility.Visible;

                    return;
                }

                var updateStudentInfo = new UserForCreation();

                if (!string.IsNullOrEmpty(NewStudentFirstName.Text))
                    updateStudentInfo.FirstName = NewStudentFirstName.Text;
                else
                    updateStudentInfo.FirstName = oldStudent.FirstName;

                if (!string.IsNullOrEmpty(NewStudentLastName.Text))
                    updateStudentInfo.LastName = NewStudentLastName.Text;
                else
                    updateStudentInfo.LastName = oldStudent.LastName;

                if (!string.IsNullOrEmpty(NewStudentFaculty.Text))
                    updateStudentInfo.Faculty = NewStudentFaculty.Text;
                else
                    updateStudentInfo.Faculty = oldStudent.Faculty;


                
                 
                if (_imagePath != null && _passportImagePath != null)
                    await studentService.UploadPicturesAsync(oldStudent.Id, _imagePath, _passportImagePath);

                else if (_imagePath != null && _passportImagePath == null ||
                    _imagePath == null && _passportImagePath != null)
                {
                    ErrorResponse.Text = "Please upload both images";
                    ErrorResponse.Visibility = Visibility.Visible;
                    
                    return;
                }

                var response = await studentService.UpdateAsync(result, updateStudentInfo);
                
                if (response is not null)
                {
                    MessageViewer messageViewer = new MessageViewer();
                    messageViewer.Show();
                }
            }
            else
            {
                NewStudentID.Text = "";
                NewStudentFaculty.Text = "";
                NewStudentFirstName.Text = "";
                NewStudentLastName.Text = "";
                ErrorResponse.Text = "Please enter a valid ID";
                ErrorResponse.Visibility = Visibility.Visible;
            }
        }

        private void PasspostImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.PNG,*.JPG;)|*.JPG;*.PNG";
            openFileDialog.InitialDirectory = Environment.GetFolderPath
                (Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                _passportImagePath = openFileDialog.FileName;

                Passblock.Text = _passportImagePath.Split('\\').Last();
            }
        }

      
    }
}