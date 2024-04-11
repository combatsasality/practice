using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.Utils.DataStructures
{
    public struct User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PrivateKey { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }

        public User (string username, string password, string email, string privateKey)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            Email = email;
            PrivateKey = privateKey;
            LastLogin = DateTime.Now;
            CreatedAt = DateTime.Now;
        
        }

        public Response UpdatePassword(string password)
        {
            if (password.Length < 4)
            {
                return new Response("register_error_password_short", false);
            }
            Password = HelpHandler.GetHashFromString(password);
            return new Response("-", true);
        }
    }
}
