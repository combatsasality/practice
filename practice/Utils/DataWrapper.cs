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
                return new Response("register_error_password_short", false);
            }
            User user = Users.FirstOrDefault(u => u.Username == username);
            if (!user.Equals(default(User)))
            {
                return new Response("register_error_user_exists", false);
            }
            if (!(new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").IsMatch(email)))
            {
                return new Response("register_error_email", false);
            }
            RSA rsa = RSA.Create();
            rsa.ExportParameters(true);
            User newUser = new User(username, HelpHandler.GetHashFromString(password), email, rsa.ToXmlString(true));
            Users.Add(newUser);
            PublicKeys.Add(new PublicKeys(newUser.Id, rsa.ToXmlString(false)));
            return new Response("-", true);
        }
        public Response Login(string username, string password)
        {
            User user = Users.Find(u => u.Username == username);
            if (user.Equals(default(User)))
            {
                return new Response("login_error_password", false);
            }
            if (user.Password != HelpHandler.GetHashFromString(password))
            {
                return new Response("login_error_password", false);
            }
            user.LastLogin = DateTime.Now;
            return new Response(true, user);
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
        public Response(bool status, User user)
        {
            this.status = status;
            this.user = user;
        }
    }
}
