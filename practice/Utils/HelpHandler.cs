using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using System.Linq;


namespace practice.Utils
{
    public static class HelpHandler
    {
        public static string PathData = @"data/data.json";
        public static JsonSerializerOptions optionsSerializer = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        public static void CreateAllStuff()
        {
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }
            string jsonString = @"{}";
            if (!File.Exists(PathData))
            {
                File.WriteAllText(PathData, jsonString);
            }
            if (!Directory.Exists(@"data/documents"))
            {
                Directory.CreateDirectory(@"data/documents/");
            }
        }

        public static string GetHashFromString(string str)
        {
            byte[] arrInput = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(str));
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        public static void ChangeMergedDictionaries(string pattern, string newSource)
        {

            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri(newSource, UriKind.Relative);

            ResourceDictionary oldDict = (
                from d in Application.Current.Resources.MergedDictionaries
                where d.Source != null && d.Source.OriginalString.StartsWith(pattern)
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
        }
        public static ResourceDictionary GetLanguageDictionary()
        {
            return (
                from d in Application.Current.Resources.MergedDictionaries
                where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Lang/")
                select d).FirstOrDefault();
        }

        public static void SaveFile(string path, string newName)
        {
            File.WriteAllBytes(@"data\documents\" + newName + Path.GetExtension(path), File.ReadAllBytes(path));
        }

    }
}
