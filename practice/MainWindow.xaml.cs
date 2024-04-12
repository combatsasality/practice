using System.Windows;
using practice.Pages;

namespace practice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
           
        }


        private void NavigationView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer.Tag.ToString())
            {
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
