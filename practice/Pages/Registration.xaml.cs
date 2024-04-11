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
            if (PasswordTextBox.Password != ConfirmPasswordTextBox.Password)
            {
                Status.Visibility = Visibility.Visible;
                Status.SetResourceReference(Label.ContentProperty, "register_error_password_not_match");
                return;
            }
            Response resp = App.Data.Register(LoginTextBox.Text, PasswordTextBox.Password, EmailTextBox.Text); ;
            if (!resp.status)
            {
                Status.Visibility = Visibility.Visible;
                Status.SetResourceReference(Label.ContentProperty, resp.message);
                return;
            }
            Authorization parentWindow = (Authorization)Window.GetWindow(this);
            Login login = new Login();
            login.LoginTextBox.Text = LoginTextBox.Text;
            login.Status.Visibility = Visibility.Visible;
            login.Status.SetResourceReference(Label.ContentProperty, "register_success");
            login.Status.Foreground = System.Windows.Media.Brushes.Green;
            parentWindow.MainFrame.Navigate(login);
        }
    }
}
