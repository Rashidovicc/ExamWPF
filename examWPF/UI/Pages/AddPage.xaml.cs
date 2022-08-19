using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using examWPF.Domain.Entities;
using examWPF.Service.DTOs.UserDTO;
using examWPF.Service.Interfaces;
using examWPF.Service.Service;
using examWPF.UI.Windows;

namespace examWPF.UI.Pages
{
    public partial class AddPage : Page
    {
        private string _passportImagePath;
        private string _imagePath;
        private MessageViewer _messageViewer;
        private readonly Attachments _attachments;
        
        public AddPage()
        {
            _attachments = new Attachments();
            _messageViewer = new MessageViewer();   
            InitializeComponent();
        }


       

        private async void Massage_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewStudentFirstName.Text) &&
                !string.IsNullOrEmpty(NewStudentLastName.Text) &&
                !string.IsNullOrEmpty(NewStudentFaculty.Text))
            {

                var newStudent = new UserForCreation()
                {
                    FirstName = NewStudentFirstName.Text,
                    LastName = NewStudentLastName.Text,
                    Faculty = NewStudentFaculty.Text,

                };
                using IUserService userService = new UserService();

                var response = await userService.CreateAsync(newStudent);

                if (response is null)
                {
                    ErrorResponse.Text = "Error when creating a new student";
                    ErrorResponse.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageViewer messageViewer = new MessageViewer();
                    messageViewer.Show();
                
                }
            }
            else
            {
               
                NewStudentFaculty.Text = "";
                NewStudentFirstName.Text = "";
                NewStudentLastName.Text = "";
                ErrorResponse.Text = "Fill all fields";
                ErrorResponse.Visibility = Visibility.Visible;
            }
           
            
        }
    }
}