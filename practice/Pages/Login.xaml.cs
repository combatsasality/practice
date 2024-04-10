using practice.Utils;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace practice.Pages
{

    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Response resp = App.Data.Login(loginTextBox.Text, passwordTextBox.Password);
            if (!resp.status)
            {
                status.Visibility = Visibility.Visible;
                status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#cf1322"));
                status.SetResourceReference(Label.ContentProperty, resp.message);
                return;
            }

            App.CurrentUser = resp.user;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Authorization parentWindow = (Authorization)Window.GetWindow(this);
            parentWindow.Close();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Authorization parentWindow = (Authorization) Window.GetWindow(this);
            parentWindow.MainFrame.Navigate(new Registration());
        }
    }
}
