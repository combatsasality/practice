using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Collections.Generic;
using DocumentWorker.Properties;
using DocumentWorker.Utils;
using System.Text.Json;
using System.IO;
using ModernWpf;
using DocumentWorker.Utils.DataStructures;
using System.Windows.Threading;


namespace DocumentWorker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// This is the entry point to the program.
    /// </summary>
    public partial class App : Application
    {
        public static DataWrapper Data;
        public static User CurrentUser;
        private static List<CultureInfo> _availableLanguages = new List<CultureInfo>();
        public static ReadOnlyCollection<CultureInfo> AvailableLanguages
        {
            get
            {
                return _availableLanguages.AsReadOnly();
            }
        }
        public App()
        {
            InitializeComponent();
            _availableLanguages.Clear();
            _availableLanguages.Add(new CultureInfo("en-US"));
            _availableLanguages.Add(new CultureInfo("uk-UA"));
            Language = Settings.Default.Language;
            Theme = Settings.Default.Theme;
            HelpHandler.CreateAllStuff();
            try
            {
                Data = JsonSerializer.Deserialize<DataWrapper>(File.ReadAllText(HelpHandler.PathData));
            }
            catch (System.Text.Json.JsonException)
            {
                Data = new DataWrapper();
            }

        }


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
                Settings.Default.Language = Language;
                Settings.Default.Save();
            }
        }
        public static ApplicationTheme Theme { get
            {
                return Settings.Default.Theme;
            } 
            set
            {
                ThemeManager.Current.ApplicationTheme = value;
                Settings.Default.Theme = value;
                Settings.Default.Save();
            }
        }


        /// <summary>
        /// Event handler for the Application Startup event, which is triggered when the application is started.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Exit += App_Exit;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Data.Save();
        }
        private void App_Exit(object sender, ExitEventArgs e)
        {
            Data.Save();
            if (Directory.Exists(HelpHandler.TempDocumentPath))
            {
                HelpHandler.ClearDirectory(HelpHandler.TempDocumentPath);
            }
        }
    }
}
