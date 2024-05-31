using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace DocumentWorker.Utils.DataStructures
{
    /// <summary>
    /// Class document represents a document in the system
    /// </summary>
    public struct Document
    {
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id == Guid.Empty)
                {
                    _id = value;
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == null)
                {
                    _name = value;
                }
                else if (_name != null)
                {
                    _name = value;
                    OnUpdate();
                }
            }
        }

        private string _nameInSystem;
        public string NameInSystem
        {
            get { return _nameInSystem; }
            set
            {
                if (_nameInSystem == null)
                {
                    _nameInSystem = value;
                }
                else if (_nameInSystem != null)
                {
                    _nameInSystem = value;
                    OnUpdate();
                }
            }
        }

        private string _originalName;
        public string OriginalName
        {
            get { return _originalName; }
            set
            {
                if (_originalName == null)
                {
                    _originalName = value;
                }
                else if (_originalName != null)
                {
                    _originalName = value;
                    OnUpdate();
                }
            }
        }

        private string _extension;
        public string Extension
        {
            get { return _extension; }
            set
            {
                if (_extension == null)
                {
                    _extension = value;
                }
                else if (_extension != null)
                {
                    _extension = value;
                    OnUpdate();
                }
            }
        }

        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set
            {
                if (_createdAt == null)
                {
                    _createdAt = value;
                }
            }
        }

        private string _hash;
        public string Hash
        {
            get { return _hash; }
            set
            {
                if (_hash == null)
                {
                    _hash = value;
                }
                else if (_hash != null)
                {
                    _hash = value;
                    OnUpdate();
                }
            }
        }

        private Guid _ownerUuid;
        public Guid OwnerUuid
        {
            get { return _ownerUuid; }
            set
            {
                if (_ownerUuid == Guid.Empty)
                {
                    _ownerUuid = value;
                }
            }
        }

        private bool _isSigned;
        public bool IsSigned
        {
            get { return _isSigned; }
            set
            {
                _isSigned = value;
            }
        }

        private List<Guid> _whoCanSign;
        public List<Guid> WhoCanSign
        {
            get { return _whoCanSign; }
            set
            {
                if (_whoCanSign == null)
                {
                    _whoCanSign = value;
                }
                else if (_whoCanSign != null)
                {
                    _whoCanSign = value;
                    OnUpdate();
                }
            }
        }

        private DateTime? _signedBefore;
        public DateTime? SignedBefore
        {
            get { return _signedBefore; }
            set
            {
                if (_signedBefore == null)
                {
                    _signedBefore = value;
                } else if (_signedBefore != null)
                {
                    _signedBefore = value;
                    OnUpdate();
                }
            }
        }

        private bool? _isArchived;
        public bool IsArchived
        {
            get { return _isArchived == null ? false : (bool) _isArchived; }
            set
            {
                if (_isArchived == null)
                {
                    _isArchived = value;
                } else if (_signedBefore != null)
                {
                    _isArchived = value;
                    OnUpdate();
                }
            }
        }

        public Document(string pathToFile, Guid ownerUuid, List<Guid> whoCanSign, string nameInSystem, DateTime? signedBefore)
        {
            _id = Guid.NewGuid();
            _name = Path.GetFileNameWithoutExtension(pathToFile) + "_unique" + _id.ToString();
            _nameInSystem = nameInSystem;
            _originalName = Path.GetFileNameWithoutExtension(pathToFile);
            _extension = Path.GetExtension(pathToFile);
            _createdAt = DateTime.Now;
            _hash = Convert.ToBase64String(SHA256.Create().ComputeHash(File.ReadAllBytes(pathToFile)));
            _ownerUuid = ownerUuid;
            _isSigned = false;
            _whoCanSign = whoCanSign;
            _signedBefore = signedBefore;
            _isArchived = false;
        }
        public void Delete()
        {
            File.Delete(Path.GetFullPath(string.Format(@"data/documents/{0}{1}", Name, Extension)));
        }
        public void SignDocument(string privateKey)
        {
            // TODO: rewrite this method
            using (RSA rsa = RSA.Create())
            {
                rsa.FromXmlString(privateKey);
                byte[] fileContent = File.ReadAllBytes(@"data\documents\" + Name + Extension);
                byte[] hashBytes = SHA256.Create().ComputeHash(fileContent);
                byte[] signature = rsa.SignHash(hashBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                File.WriteAllBytes(@"data\documents\" + Name + Extension + ".pem", signature);
            }
            OnUpdate();
        }
        public void GetSignAndDocument(string path)
        {
            // TODO: rewrite this method
            File.WriteAllBytes(path + @"\" + NameInSystem + Extension, File.ReadAllBytes(@"data\documents\" + Name + Extension));
            File.WriteAllBytes(path + @"\" + NameInSystem + Extension + ".pem", File.ReadAllBytes(@"data\documents\" + Name + Extension + ".pem"));
        }
        public void OnUpdate()
        {
            Guid id = Id;
            App.Data.Documents[App.Data.Documents.FindIndex(d => d.Id == id)] = this;
            App.Data.Save();
        }
    }

}
