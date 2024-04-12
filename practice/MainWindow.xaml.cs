using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using practice.Pages;
using practice.Utils;

namespace practice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
            Menu.SelectedItem = MenuHome;
        }


        private void NavigationView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer == null) return;
            switch (args.SelectedItemContainer.Tag.ToString())
            {
                case "home":
                    ContentFrame.Navigate(new Home());
                    Menu.SetResourceReference(ModernWpf.Controls.NavigationView.HeaderProperty, "menu_home");
                    break;
                case "settings":
                    ContentFrame.Navigate(new Settings());
                    Menu.SetResourceReference(ModernWpf.Controls.NavigationView.HeaderProperty, "menu_settings");
                    break;
                case "add_document":
                    ContentFrame.Navigate(new AddDocument());
                    Menu.SetResourceReference(ModernWpf.Controls.NavigationView.HeaderProperty, "menu_add_document");
                    break;
                case "documents":
                    ContentFrame.Navigate(new Documents());
                    Menu.SetResourceReference(ModernWpf.Controls.NavigationView.HeaderProperty, "menu_documents");
                    break;
                case "check_sign":
                    HelpHandler.CheckSignPath();
                    sender.SelectedItem = null;
                    break;
                case "logout":
                    // Maybe add a confirmation dialog or flyout
                    Logout();
                    break;
            }
        }
        private void Logout()
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            App.CurrentUser = default;
            Close();
        }
    }
}
