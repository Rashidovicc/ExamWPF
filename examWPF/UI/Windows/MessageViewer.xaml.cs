using System.Windows;

namespace examWPF.UI.Windows
{
    public partial class MessageViewer : Window
    {
        public MessageViewer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }
    }
}