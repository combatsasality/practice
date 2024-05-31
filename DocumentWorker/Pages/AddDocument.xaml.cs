using Microsoft.Win32;
using DocumentWorker.Utils;
using DocumentWorker.Utils.DataStructures;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;

namespace DocumentWorker.Pages
{
    /// <summary>
    /// Interaction logic for AddDocument.xaml
    /// </summary>
    public partial class AddDocument : Page
    {
        public StreamReader SelectedDocument;
        public string FilePath;
        public AddDocument()
        {
            InitializeComponent();
            DataContext = this;
            UserListView.ItemsSource = App.Data.Users.FindAll(u => u.Id != App.CurrentUser.Id);
        }

        private void SelectDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                SelectedDocument = new StreamReader(fileDialog.FileName);
                FilePath = fileDialog.FileName;
                ChangeVisibility(Visibility.Visible);

            }
        }
        
        private void ChangeVisibility(Visibility visibility)
        {
            SelectDocumentButton.Visibility = visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            FirstPanel.Visibility = visibility;
            SecondPanel.Visibility = visibility;
            AddDocumentButton.Visibility = visibility;
        }


        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (SelectedDocument != null)
            {
                SelectedDocument.Close();
            }
        }

        private void AddDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resources = HelpHandler.GetLanguageDictionary();
            if (DocumentNameTextBox.Text == "")
            {
                MessageBox.Show((string)resources["adddocument_not_named_document"]);
                return;
            }
            if (UserListView.SelectedItem == null)
            {
                MessageBox.Show((string)resources["adddocument_not_selected_user_document"]);
                return;
            }
            if (DocumentDateDatePicker.SelectedDate < DateTime.Now)
            {
                MessageBox.Show((string)resources["adddocument_invalid_selected_date_document"]);
                return;
            }
            List<Guid> guids = new List<Guid>();
            foreach (User user in UserListView.SelectedItems)
            {
                guids.Add(user.Id);
            }
            SelectedDocument.Close();
            Document document = new Document(FilePath, App.CurrentUser.Id, guids, DocumentNameTextBox.Text, DocumentDateDatePicker.SelectedDate);
            App.Data.Documents.Add(document);
            HelpHandler.SaveFile(FilePath, document.Name);
            SelectedDocument = null;
            FilePath = null;
            UserListView.SelectedValue = null;
            DocumentNameTextBox.Text = "";
            DocumentDateDatePicker.SelectedDate = null;
            ChangeVisibility(Visibility.Hidden);
        }
    }
}
