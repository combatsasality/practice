using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using practice.Properties;
using practice.Utils;
using System.Text.Json;
using System.IO;
using practice.Utils.DataStructures;


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
            _availableLanguages.Clear();
            _availableLanguages.Add(new CultureInfo("en-US"));
            _availableLanguages.Add(new CultureInfo("uk-UA"));

            Language = Settings.Default.Language;
            HelpHandler.CreateAllStuff();
            Data = JsonSerializer.Deserialize<DataWrapper>(File.ReadAllText(HelpHandler.PathData));
            Data.Documents.Add(new Document(@"E:\download\Telegram Desktop\image_2024-04-09_14-31-40.png", Data.Users[0].Id, new List<Guid>() { Data.Users[1].Id, Data.Users[2].Id }));

            //Response resp = Data.Register("admin", "admin", "admin@admin.com");
            //if (!resp.status)
            //{
            //    MessageBox.Show(resp.message);
            //}
            //Data.Register("JohnDoe", "john", "johndoe@gmail.com");
            //Data.Register("Valera", "valera", "m3sckt@gmail.com");
            //Data.Register("Onervi", "admin", "onervi@combatsasality.com");
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
