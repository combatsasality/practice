using System.Windows.Controls;
using System.Windows;
using practice.Pages;
using practice.Utils;

namespace practice
{
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Authorization parentWindow = (Authorization)Window.GetWindow(this);
            parentWindow.MainFrame.Navigate(new Login());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passwordTextBox.Password != confirmPasswordTextBox.Password)
            {
                status.Visibility = Visibility.Visible;
                status.SetResourceReference(Label.ContentProperty, "register_error_password_not_match");
                return;
            }
            Response resp = App.Data.Register(loginTextBox.Text, passwordTextBox.Password, emailTextBox.Text);
            if (!resp.status)
            {
                status.Visibility = Visibility.Visible;
                status.SetResourceReference(Label.ContentProperty, resp.message);
                return;
            }
            Authorization parentWindow = (Authorization)Window.GetWindow(this);
            Login login = new Login();
            login.loginTextBox.Text = loginTextBox.Text;
            login.status.Visibility = Visibility.Visible;
            login.status.SetResourceReference(Label.ContentProperty, "register_success");
            login.status.Foreground = System.Windows.Media.Brushes.Green;
            parentWindow.MainFrame.Navigate(login);
        }
    }
}
