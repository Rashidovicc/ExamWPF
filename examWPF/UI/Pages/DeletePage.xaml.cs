using System.Windows;
using System.Windows.Controls;
using examWPF.Domain.Commons;
using examWPF.Service.Interfaces;
using examWPF.Service.Service;
using examWPF.UI.Windows;

namespace examWPF.UI.Pages
{
    public partial class DeletePage : Page
    {
       public DeletePage()
        {
            
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var IsValidId = long.TryParse(TxtDelete.Text, out long result);
            
            if (IsValidId)
            {
                using IUserService studentService = new UserService();
                
                var response = await studentService.DeleteAsync(result);

                if (!response)
                {
                    ErrorResponse.Text = "Student not found";
                    ErrorResponse.Visibility = Visibility.Visible;
                }

                else
                {
                    TxtDelete.Text = "";
                    MessageViewer messages = new MessageViewer();
                    messages.Show();
                }
            }
            
            else
            {
                ErrorResponse.Text = "Invalid Id";
                ErrorResponse.Visibility = Visibility.Visible;
            }
            
        }
    }
}