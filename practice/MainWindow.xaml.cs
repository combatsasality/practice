using System.Windows;
using System.Windows.Controls;
using ModernWpf.Controls.Primitives;
using practice.Pages;
using practice.Utils.DataStructures;

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
