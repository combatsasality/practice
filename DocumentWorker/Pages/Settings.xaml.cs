﻿using System.Globalization;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using ModernWpf;
using DocumentWorker.Utils;

namespace DocumentWorker.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            CultureInfo currentLangauge = App.Language;
            DataContext = this;
            ComboBoxLanguage.Items.Clear();
            foreach (var lang in App.AvailableLanguages)
            {
                ComboBoxItem itemLang = new ComboBoxItem();
                itemLang.Content = lang.DisplayName;
                itemLang.Tag = lang;
                itemLang.IsSelected = lang.Equals(currentLangauge);
                itemLang.Selected += ChangeLanguageClick;
                ComboBoxLanguage.Items.Add(itemLang);
            }
            List<ApplicationTheme> themes = new List<ApplicationTheme> { ApplicationTheme.Light, ApplicationTheme.Dark };
            foreach (var theme in themes)
            {
                ComboBoxItem itemTheme = new ComboBoxItem();
                itemTheme.SetResourceReference(ComboBoxItem.ContentProperty, string.Format("text_menu_theme_{0}", theme.ToString().ToLower()));
                itemTheme.Tag = theme;
                itemTheme.IsSelected = theme.Equals(App.Theme);
                itemTheme.Selected += ChangeThemeClick;
                ComboBoxTheme.Items.Add(itemTheme);
            }

        }

        private void ChangeLanguageClick(object sender, EventArgs e)
        {
            ComboBoxItem languageMenu = sender as ComboBoxItem;
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
            ComboBoxItem themeMenu = sender as ComboBoxItem;
            if (themeMenu != null)
            {
                if (themeMenu.Tag is ApplicationTheme theme)
                {
                    App.Theme = theme;
                }
            }
        }

        private void ButtonChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resources = HelpHandler.GetLanguageDictionary();
            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show((string)resources["register_error_password_not_match"], "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Response resp = App.CurrentUser.ChangePassword(PasswordBox.Password);
            string locValue = (string)resources[resp.message];
            if (!resp.status)
            {
                MessageBox.Show(locValue, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show(locValue, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
