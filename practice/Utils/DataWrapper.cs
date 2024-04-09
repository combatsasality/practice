using practice.Utils.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace practice.Utils
{

    public class DataWrapper
    {
        public List<User> Users { get; set; } = new List<User>();
        public List<Document> Documents { get; set; } = new List<Document>();
        public List<PublicKeys> PublicKeys { get; set; } = new List<PublicKeys>();
        public void Save()
        {
            File.WriteAllText(HelpHandler.PathData, JsonSerializer.Serialize(this, HelpHandler.optionsSerializer));
        }

        public Response Register(string username, string password, string email)
        {
            if (username.Length < 4 || password.Length < 4)
            {
                return new Response("Логін або пароль занадто короткий", false);
            }
            User user = Users.FirstOrDefault(u => u.Username == username);
            if (!user.Equals(default(User)))
            {
                return new Response("Користувач з таким логіном вже існує", false);
            }
            if (!(new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").IsMatch(email)))
            {
                return new Response("Некоректний email", false);
            }
            RSA rsa = RSA.Create();
            rsa.ExportParameters(true);
            User newUser = new User(username, HelpHandler.GetHashFromString(password), email, rsa.ToXmlString(true));
            Users.Add(newUser);
            PublicKeys.Add(new PublicKeys(newUser.Id, rsa.ToXmlString(false)));
            return new Response("Користувач успішно зареєстрований", true);
        }
    }


    public class Response
    {
        public string message;
        public bool status;
        public User user;
        public Response(string message, bool status)
        {
            this.message = message;
            this.status = status;
        }
        public Response(string message, bool status, User user)
        {
            this.message = message;
            this.status = status;
            this.user = user;
        }
    }
}
