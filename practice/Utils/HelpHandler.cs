using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

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

    }
}
