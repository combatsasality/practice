using DocumentWorker.Utils;
using DocumentWorker.Utils.DataStructures;
using System;
using System.IO;
using System.Windows;

namespace DocumentWorker.Windows
{
    /// <summary>
    /// Class UnarchiveDialogBox represents a dialog box for unarchiving a document
    /// </summary>
    public partial class UnarchiveDialogBox : Window
    {
        private Document _document;
        public UnarchiveDialogBox(Document document)
        {
            InitializeComponent();
            DataContext = this;
            UserListView.ItemsSource = App.Data.Users.FindAll(u => u.Id != App.CurrentUser.Id);
            _document = document;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resources = HelpHandler.GetLanguageDictionary();
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
            _document.WhoCanSign.Add(((User)UserListView.SelectedItem).Id);
            _document.SignedBefore = DocumentDateDatePicker.SelectedDate;
            _document.IsArchived = false;
            _document.IsSigned = false;
            File.Delete(@"data\documents\" + _document.Name + _document.Extension + ".pem");
            Close();
        }

    }
}
