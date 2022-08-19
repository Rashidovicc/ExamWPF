using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using examWPF.Domain.Entities;
using examWPF.Service.Interfaces;
using examWPF.Service.Service;
using examWPF.UI.Controls;
using examWPF.UI.Pages;
using Newtonsoft.Json;

namespace examWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUserService _userService;
        private readonly SavePage _savePage;
        private readonly DeletePage _deletePage;
        private readonly AddPage _addPage;
        private Thread _thread;
        
        public MainWindow()
        {
            _userService = new UserService();
            _deletePage =new DeletePage();
            _savePage = new SavePage();
            _addPage = new AddPage();
            InitializeComponent();
        }


        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ExitBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimzeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizedBase_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFrame.Content = _savePage;
        }
        private IEnumerable<User> _allStudents;
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // ReSharper disable once AsyncVoidLambda
            _thread = new Thread(async () =>
            {
                Dispatcher.Invoke(() => UsersList.Items.Clear());


                _allStudents = await _userService.GetAllAsync();

                await LoadStudents(_allStudents);
            });

            _thread.Start();
            
        }

        private async Task LoadStudents(IEnumerable<User> allStudents)
        {
            foreach (var user in allStudents)
            {
                await this.Dispatcher.InvokeAsync(() =>
                {
                    UserInfo privateChat = new UserInfo();
                    
                    privateChat.NameTxt.Text = user.FirstName + " " + user.LastName;
                    privateChat.ID.Text = user.Id.ToString();
                    privateChat.FacultyMsgTxt.Text = user.Faculty;
                    privateChat.DateTimeTxt.Text = user.CreatedAt.ToString(CultureInfo.InvariantCulture);
                    privateChat.UserImg.ImageSource = user.Image is not null
                        ? new BitmapImage(new Uri("https://talabamiz.uz/" + user.Image.Path))
                        : new BitmapImage(new Uri("https://talabamiz.uz/Images//99daf8ac38de4433aa36a61baf4c9c4d.png"));

                    UsersList.Items.Add(privateChat);
                });
            }
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFrame.Content = _deletePage;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFrame.Content = _addPage;
        }

        private void SearchTxt_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UsersList.Items.Clear();

            string text = SearchTxt.Text.ToLower();


            // ReSharper disable once AsyncVoidLambda
            _thread = new Thread(async () =>
            {
                _allStudents = await _userService.GetAllAsync();

                _allStudents = _allStudents.Where(p => p.FirstName.ToLower().Contains(text)
                                                     || p.LastName.ToLower().Contains(text)
                                                     || p.Faculty.ToLower().Contains(text));

                await LoadStudents(_allStudents);
            });
            _thread.Start();
        }
    }
}