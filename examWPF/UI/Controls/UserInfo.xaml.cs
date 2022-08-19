using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using examWPF.Service.Service;

namespace examWPF.UI.Controls
{
    public partial class UserInfo : UserControl
    {
        private readonly UserService _userService;
        public UserInfo()
        {
            _userService = new UserService();
            InitializeComponent();
        }


        private async void ID_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Windows.UserInfo info = new Windows.UserInfo();
            
            var userInfo = (UserInfo)sender;
            
            
            var res =await (_userService.GetAsync(long.Parse(userInfo.ID.Text)));
           
            info.Image.ImageSource = res.Image is not null
                ? new BitmapImage(new Uri("https://talabamiz.uz/" + res.Image.Path))
                : new BitmapImage(new Uri("https://talabamiz.uz/Images//99daf8ac38de4433aa36a61baf4c9c4d.png"));
           
            info.PassportImage.ImageSource = res.Image is not null
                ? new BitmapImage(new Uri("https://talabamiz.uz/" + res.Image.Path))
                : new BitmapImage(new Uri("https://talabamiz.uz/Images//99daf8ac38de4433aa36a61baf4c9c4d.png"));
           
            info.Firstname.Text = res.FirstName;
            info.LastName.Text = res.LastName;
            info.UserId.Text = res.Id.ToString();
            info.Faculty.Text = res.Faculty;

            info.Show();
        }

    }
}