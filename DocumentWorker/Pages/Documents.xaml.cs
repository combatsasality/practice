using DocumentWorker.Utils.DataStructures;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using DocumentWorker.Windows;

namespace DocumentWorker.Pages
{
    /// <summary>
    /// Interaction logic for Documents.xaml
    /// </summary>
    public partial class Documents : Page
    {
        public Documents()
        {
            InitializeComponent();
            DataContext = this;
            UpdateAllDataGrid();
        }

        public void UpdateAllDataGrid()
        {
            ArchivedDocumentsDataGrid.ItemsSource = App.Data.Documents.FindAll(d => d.OwnerUuid == App.CurrentUser.Id && d.IsArchived);
            AddedDocumentsDataGrid.ItemsSource = App.Data.Documents.FindAll(d => d.OwnerUuid == App.CurrentUser.Id && !d.IsArchived);
            ToSignDocumentsDataGrid.ItemsSource = App.Data.Documents.FindAll(d => d.WhoCanSign.Contains(App.CurrentUser.Id) && (d.SignedBefore > DateTime.Now || d.SignedBefore == null) && !d.IsArchived);
        }

        private void MenuItemPreviewClick_ArchivedDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            foreach (Document document in ArchivedDocumentsDataGrid.SelectedItems)
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
        private void MenuGetDocumentClick_ArchivedDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (Document document in ArchivedDocumentsDataGrid.SelectedItems)
                {
                    if (document.IsSigned) document.GetSignAndDocument(dialog.FileName);
                }
            }
        }
        private void MenuItemUnarchiveClick_ArchivedDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            foreach (Document document in ArchivedDocumentsDataGrid.SelectedItems)
            {
                parentWindow.IsEnabled = false;
                Window dialogBox = new UnarchiveDialogBox(document);
                dialogBox.Closed += (s, args) =>
                {
                    parentWindow.IsEnabled = true;
                };
                dialogBox.ShowDialog();
            }
            UpdateAllDataGrid();
        }
        private void MenuItemDeleteClick_ArchivedDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            foreach (Document document in ArchivedDocumentsDataGrid.SelectedItems)
            {
                document.Delete();
                App.Data.Documents.Remove(document);
            }
            UpdateAllDataGrid();
        }
        private void MenuItemArchiveClick_AddedDocumentsDataGrid(object sender, RoutedEventArgs e)
        {
            foreach (Document document in AddedDocumentsDataGrid.SelectedItems)
            {
                Document current = App.Data.Documents.Find(d => d.Id == document.Id);
                current.IsArchived = true;
                current.SignedBefore = null;
                current.WhoCanSign.Clear();
            }
            UpdateAllDataGrid();
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
            UpdateAllDataGrid();
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
