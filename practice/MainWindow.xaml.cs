using System.Windows;
using System.Windows.Controls;
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
            //ContentFrame.Navigate(new Settings());
            //ContentFrame.Content = new TextBlock { Text = args.SelectedItemContainer.Tag.ToString() };
            switch (args.SelectedItemContainer.Tag.ToString())
            {
                case "settings":
                    ContentFrame.Navigate(new Settings());
                    Menu.SetResourceReference(ModernWpf.Controls.NavigationView.HeaderProperty, "menu_settings");
                    break;
                case "logout":
                    Authorization authorization = new Authorization();
                    authorization.Show();
                    App.CurrentUser = default;
                    Close();
                    break;
            }
        }
    }
}
