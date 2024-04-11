using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace practice.Pages
{
    public partial class AddDocument : Page
    {
        public StreamReader SelectedDocument;
        public AddDocument()
        {
            InitializeComponent();
            DataContext = this;
            UserListView.ItemsSource = App.Data.Users;
        }

        private void SelectDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                SelectedDocument = new StreamReader(fileDialog.FileName);
                SelectDocumentButton.Visibility = Visibility.Hidden;
                FirstPanel.Visibility = Visibility.Visible;
                SecondPanel.Visibility = Visibility.Visible;
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (SelectedDocument != null)
            {
                SelectedDocument.Close();
            }
        }
    }
}
