using practice.Utils.DataStructures;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace practice.Pages
{
    public partial class Documents : Page
    {
        public Documents()
        {
            InitializeComponent();
            DataContext = this;
            AddedDocumentsDataGrid.ItemsSource = App.Data.Documents.FindAll(d => d.OwnerUuid == App.CurrentUser.Id);
        }

        private void MenuItemPreviewClick(object sender, RoutedEventArgs e)
        {
            foreach (Document document in AddedDocumentsDataGrid.SelectedItems)
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo(Path.GetFullPath(string.Format(@"data/documents/{0}{1}", document.Name, document.Extension)))
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            foreach (Document document in AddedDocumentsDataGrid.SelectedItems)
            {
                document.Delete();
                App.Data.Documents.Remove(document);
            }
            AddedDocumentsDataGrid.ItemsSource = App.Data.Documents.FindAll(d => d.OwnerUuid == App.CurrentUser.Id);
        }
    }
}
