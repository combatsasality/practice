using System;
using System.Windows;
using System.Windows.Controls;
using practice.Utils;

namespace practice
{

    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            MainFrame.Navigate(new Uri("Pages/Login.xaml", UriKind.Relative));
        }

    }
}
