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
            Response resp = App.Data.Login(LoginTextBox.Text, PasswordTextBox.Password);
            if (!resp.status)
            {
                Status.Visibility = Visibility.Visible;
                Status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#cf1322"));
                Status.SetResourceReference(Label.ContentProperty, resp.message);
                return;
            }

            App.CurrentUser = resp.user;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Authorization parentWindow = (Authorization) Window.GetWindow(this);
            parentWindow.MainFrame.Navigate(new Registration());
        }
    }
}
