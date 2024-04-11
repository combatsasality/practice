using System.Globalization;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using ModernWpf;

namespace practice.Pages
{
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            CultureInfo currentLangauge = App.Language;
            menuLanguage.Items.Clear();
            foreach (var lang in App.AvailableLanguages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currentLangauge);
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);
            }
            menuTheme.Items.Clear();
            List<ApplicationTheme> themes = new List<ApplicationTheme> { ApplicationTheme.Light, ApplicationTheme.Dark };
            foreach (var theme in themes)
            {
                MenuItem menuThemeItem = new MenuItem();
                menuThemeItem.SetResourceReference(MenuItem.HeaderProperty, string.Format("text_menu_theme_{0}", theme.ToString().ToLower()));
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
                if (themeMenu.Tag is ApplicationTheme theme)
                {
                    App.Theme = theme;
                }
            }
        }
    }
}
