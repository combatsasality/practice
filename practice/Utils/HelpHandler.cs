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
using practice.Utils.DataStructures;
using Microsoft.Win32;


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

        public static void CheckSignPath()
        {
            ResourceDictionary lang = GetLanguageDictionary();

            OpenFileDialog documentDialog = new OpenFileDialog();
            if (documentDialog.ShowDialog() == false)
            {
                return;
            }
            string PathDocument = documentDialog.FileName;
            OpenFileDialog signDialog = new OpenFileDialog();
            signDialog.Filter = "Encrypted files (*.pem)|*.pem";
            if (signDialog.ShowDialog() == false)
            {
                return;
            }
            string PathSign = signDialog.FileName;
            try { 
            using (RSA rsa = RSA.Create())
            {
                bool isSignatureValid = false;
                byte[] hashBytes;
                byte[] signatureBytes = File.ReadAllBytes(PathSign);

                using (SHA256 sha256 = SHA256.Create())
                {
                    hashBytes = sha256.ComputeHash(File.ReadAllBytes(PathDocument));
                }

                foreach (PublicKeys publicKey in App.Data.PublicKeys)
                {
                    rsa.FromXmlString(publicKey.PublicKey);
                    isSignatureValid = rsa.VerifyHash(hashBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    if (isSignatureValid)
                    {
                        break;
                    }

                }
                if (isSignatureValid)
                    {
                        MessageBox.Show((string)lang["checksign_sign_correct"]);
                        return;
                    }
                }
            } catch (Exception e) when (e is ArgumentException || e is FileNotFoundException)
            {
                MessageBox.Show((string)lang["checksign_file_not_found"]);
            }
            catch (Exception e) when (e is DirectoryNotFoundException || e is IOException)
            {
                MessageBox.Show((string)lang["checksign_not_correct_path"]);
            }
            catch (Exception e) when (e is UnauthorizedAccessException)
            {
                MessageBox.Show((string)lang["checksign_no_unauthorized_access"]);
            }

            MessageBox.Show((string)lang["checksign_sign_correct"]);
        }
    }
}
