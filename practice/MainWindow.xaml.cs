using System.Linq;
using System;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;

namespace practice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            App.LanguageChanged += LanguageChanged;
            InitializeComponent();
            CultureInfo currentLangauge = App.Language;
            menuLanguage.Items.Clear();
            foreach (var lang in App.availableLanguages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currentLangauge);
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);

            }
        }

        private void ChangeLanguageClick(object sender, EventArgs e)
        {
            MenuItem languageMneu = sender as MenuItem;
            if (languageMneu != null)
            {
                CultureInfo lang = languageMneu.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
        }
        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in menuLanguage.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

    }
}
