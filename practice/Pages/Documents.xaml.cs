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


    }
}
