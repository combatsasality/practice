using System.Linq;
using System;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using System.Collections.Generic;

namespace practice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            App.LanguageChanged += LanguageChanged;
            App.ThemeChanged += ThemeChanged;
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
            menuTheme.Items.Clear();
            List<string> themes = new List<string> { "Light", "Dark" };
            foreach (var theme in themes)
            {
                MenuItem menuThemeItem = new MenuItem();
                menuThemeItem.SetResourceReference(MenuItem.HeaderProperty, string.Format("text_menu_theme_{0}", theme.ToLower()));
                menuThemeItem.Tag = theme;
                menuThemeItem.IsChecked = theme.Equals(App.Theme);
                menuThemeItem.Click += ChangeThemeClick;
                menuTheme.Items.Add(menuThemeItem);
            }
        }


        private void ChangeLanguageClick(object sender, EventArgs e)
        {
            MenuItem languageMenu = sender as MenuItem;
            if (languageMenu != null)
            {
                if (languageMenu.Tag is CultureInfo lang)
                {
                    App.Language = lang;
                }
            }
        }
        private void ChangeThemeClick(object sender, EventArgs e)
        {
            MenuItem themeMenu = sender as MenuItem;
            if (themeMenu != null)
            {
                if (themeMenu.Tag is string theme)
                {
                    App.Theme = theme;
                }
            }
        }
        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;
            foreach (MenuItem i in menuLanguage.Items)
            {
                i.IsChecked = i.Tag is CultureInfo ci && ci.Equals(currLang);
            }
        }

        private void ThemeChanged(Object sender, EventArgs e)
        {
            string currTheme = App.Theme;

            foreach (MenuItem i in menuTheme.Items)
            {
                i.IsChecked = i.Tag is string theme && theme.Equals(currTheme);
            }
        }

    }
}
