using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.Utils.DataStructures
{
    public struct User
    {
        public Guid _id;
        public Guid Id { get { return _id; } set { 
                if (_id == Guid.Empty)
                {
                    _id = value;
                }
            } }
        private string _username;

        public string Username { get { return _username; } set
            {
                if (_username == null)
                {
                    _username = value;
                } else if (_username != null)
                {
                    _username = value;
                    OnUpdate();
                }

            } }

        private string _password;
        
        public string Password { get { return _password; } set
            {
                if (_password == null)
                {
                    _password = value;
                } else if (_password != null)
                {
                    _password = value;
                    OnUpdate();
                }
            } }

        private string _email;

        public string Email { get { return _email; } set
            {
                if (_email == null)
                {
                    _email = value;
                } else if (_email != null)
                {
                    _email = value;
                    OnUpdate();
                }
            } }

        private string _privateKey;
        public string PrivateKey { get { return _privateKey; } set
            {
                if (_privateKey == null)
                {
                    _privateKey = value;
                }
            }
        }
        private DateTime? _lastLogin;

        public DateTime? LastLogin { get
            {
                return _lastLogin;
            } set
            {
                if (_lastLogin == null)
                {
                    _lastLogin = value;
                }
                else if (_lastLogin != null)
                {
                    _lastLogin = value;
                    OnUpdate();
                }
            }
            }

        private DateTime? _createdAt;
        public DateTime? CreatedAt { get
            {
                return _createdAt;
            }
            set
            {
                if (_createdAt == null)
                {
                    _createdAt = value;
                }
            }
        }

        public User (string username, string password, string email, string privateKey)
        {
            _id = Guid.NewGuid();
            _username = username;
            _password = password;
            _email = email;
            _privateKey = privateKey;
            _lastLogin = DateTime.Now;
            _createdAt = DateTime.Now;
        
        }

        public Response ChangePassword(string password)
        {
            if (password.Length < 4)
            {
                return new Response("register_error_password_short", false);
            }
            this.Password = HelpHandler.GetHashFromString(password);
            return new Response("password_succ_changed", true);
        }

        public void OnUpdate()
        {
            Guid id = Id;
            App.Data.Users[App.Data.Users.FindIndex(u => u.Id == id)] = this;
            App.Data.Save();
        }
    }
}
