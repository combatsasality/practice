using System;
using System.Windows;

namespace DocumentWorker
{
    /// <summary>
    /// Class Authorization represents the authorization window
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            MainFrame.Navigate(new Uri("Pages/Login.xaml", UriKind.Relative));
        }

    }
}
