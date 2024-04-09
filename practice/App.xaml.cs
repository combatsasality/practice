using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using practice.Properties;


namespace practice
{
    public partial class App : Application
    {
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
            _availableLanguages.Clear();
            _availableLanguages.Add(new CultureInfo("en-US"));
            _availableLanguages.Add(new CultureInfo("uk-UA"));

            Language = Settings.Default.Language;
        }
        public static event EventHandler LanguageChanged;
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

                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(String.Format("Resources/lang/{0}.xaml", value.Name), UriKind.Relative);

                ResourceDictionary oldDict = (
                    from d in Application.Current.Resources.MergedDictionaries
                    where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang/")
                    select d).FirstOrDefault();
                if (oldDict != null)
                {
                    int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(index, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }
                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            Settings.Default.Language = Language;
            Settings.Default.Save();
        }
    }
}
