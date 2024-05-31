using System.Windows;
using DocumentWorker.Pages;
using DocumentWorker.Utils;
using ModernWpf.Controls;

namespace DocumentWorker
{
    /// <summary>
    /// Main window of the application
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
            Menu.SelectedItem = MenuHome;
        }

        /// <summary>
        /// Event handler for the NavigationView SelectionChanged event, which is triggered when the user selects a menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer == null) return;
            switch (args.SelectedItemContainer.Tag.ToString())
            {
                case "home":
                    ContentFrame.Navigate(new Home());
                    Menu.SetResourceReference(NavigationView.HeaderProperty, "menu_home");
                    break;
                case "settings":
                    ContentFrame.Navigate(new Settings());
                    Menu.SetResourceReference(NavigationView.HeaderProperty, "menu_settings");
                    break;
                case "add_document":
                    ContentFrame.Navigate(new AddDocument());
                    Menu.SetResourceReference(NavigationView.HeaderProperty, "menu_add_document");
                    break;
                case "documents":
                    ContentFrame.Navigate(new Documents());
                    Menu.SetResourceReference(NavigationView.HeaderProperty, "menu_documents");
                    break;
                case "check_sign":
                    HelpHandler.CheckSignPath();
                    sender.SelectedItem = null;
                    break;
                case "logout":
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
