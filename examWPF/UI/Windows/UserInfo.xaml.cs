using System.Windows;
using System.Windows.Input;

namespace examWPF.UI.Windows
{
    public partial class UserInfo : Window
    {
        public UserInfo()
        {
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}