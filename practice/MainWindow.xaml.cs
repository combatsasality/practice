using System.Linq;
using System;
using System.Windows;
using System.Globalization;

namespace practice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            App.Language = new CultureInfo("uk-UA");
        }
    }
}
