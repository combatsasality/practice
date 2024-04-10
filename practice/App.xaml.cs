using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Collections.Generic;
using practice.Properties;
using practice.Utils;
using System.Text.Json;
using System.IO;


namespace practice
{
    public partial class App : Application
    {
        public static DataWrapper Data;
        private static List<CultureInfo> _availableLanguages = new List<CultureInfo>();
        public static ReadOnlyCollection<CultureInfo> availableLanguages
        {
            get
            {
                return _availableLanguages.AsReadOnly();
            }
        }

        public App()
        {
            InitializeComponent();
            App.LanguageChanged += App_LanguageChanged;
            App.ThemeChanged += App_ThemeChanged;
            _availableLanguages.Clear();
            _availableLanguages.Add(new CultureInfo("en-US"));
            _availableLanguages.Add(new CultureInfo("uk-UA"));

            Language = Settings.Default.Language;
            Theme = Settings.Default.Theme;
            HelpHandler.CreateAllStuff();
            Data = JsonSerializer.Deserialize<DataWrapper>(File.ReadAllText(HelpHandler.PathData));
        }

        private void App_ThemeChanged(object sender, EventArgs e)
        {
            // NO-OP
        }

        public static event EventHandler LanguageChanged;
        public static event EventHandler ThemeChanged;
        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;
                HelpHandler.ChangeMergedDictionaries("Resources/Lang/", String.Format("Resources/Lang/{0}.xaml", value.Name));

                LanguageChanged(Application.Current, new EventArgs());
            }
        }
        public static string Theme { get
            {
                return Settings.Default.Theme;
            } 
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                HelpHandler.ChangeMergedDictionaries("Resources/Theme/", String.Format("Resources/Theme/{0}.xaml", value));

                Settings.Default.Theme = value;
                Settings.Default.Save();
                ThemeChanged(Application.Current, new EventArgs());
            }
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            Settings.Default.Language = Language;
            Settings.Default.Save();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Exit += App_Exit;
        }
        private void App_Exit(object sender, ExitEventArgs e)
        {
            Data.Save();
        }
    }
}
