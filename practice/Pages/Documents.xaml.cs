using practice.Utils.DataStructures;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace practice.Pages
{
    public partial class Documents : Page
    {
        public Documents()
        {
            InitializeComponent();
            DataContext = this;
            AddedDocumentsDataGrid.ItemsSource = App.Data.Documents.FindAll(d => d.OwnerUuid == App.CurrentUser.Id);
            ToSignDocumentsDataGrid.ItemsSource = App.Data.Documents.FindAll(d => d.WhoCanSign.Contains(App.CurrentUser.Id) && (d.SignedBefore > DateTime.Now || d.SignedBefore == null));
        }

        private void MenuItemPreviewClick_AddedDocumentsDataGrid(object sender, RoutedEventArgs e)
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
        private void MenuItemDeleteClick_AddedDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            foreach (Document document in AddedDocumentsDataGrid.SelectedItems)
            {
                document.Delete();
                App.Data.Documents.Remove(document);
            }
            AddedDocumentsDataGrid.ItemsSource = App.Data.Documents.FindAll(d => d.OwnerUuid == App.CurrentUser.Id);
        }
        private void MenuGetDocumentClick_AddedDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (Document document in AddedDocumentsDataGrid.SelectedItems)
                {
                    if (document.IsSigned) document.GetSignAndDocument(dialog.FileName);
                }
            }
        }
        private void MenuItemPreviewClick_ToSignDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            foreach (Document document in ToSignDocumentsDataGrid.SelectedItems)
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
        private void MenuItemSignClick_ToSignDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            // Where are my java iterators T.T
            foreach (Document document in ToSignDocumentsDataGrid.SelectedItems)
            {
                Document current = App.Data.Documents.Find(d => d.Id == document.Id);
                if (current.IsSigned)
                {
                    continue;
                }
                current.SignDocument(App.CurrentUser.PrivateKey);
                current.IsSigned = true;
                current.SignedBefore = null;
            }
            ToSignDocumentsDataGrid.ItemsSource = App.Data.Documents.FindAll(d => d.WhoCanSign.Contains(App.CurrentUser.Id) && (d.SignedBefore > DateTime.Now || d.SignedBefore == null));

        }
        private void MenuGetDocumentClick_ToSignDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (Document document in ToSignDocumentsDataGrid.SelectedItems)
                {
                    if (document.IsSigned) document.GetSignAndDocument(dialog.FileName);
                }

            }
        }
    }
}
