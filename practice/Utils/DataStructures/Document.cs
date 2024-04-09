using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace practice.Utils.DataStructures
{
    public struct Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set;}
        public string Extension { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Hash { get; set; }
        public Guid OwnerUuid { get; set; }
        public bool IsSigned { get; set; }
        public List<Guid> WhoCanSign { get; set; }
        public Document(string pathToFile, Guid ownerUuid, List<Guid> whoCanSign)
        {
            OwnerUuid = ownerUuid;
            WhoCanSign = whoCanSign;
            Id = Guid.NewGuid();
            Name = Path.GetFileNameWithoutExtension(pathToFile) + "_unique" + Id.ToString();
            OriginalName = Path.GetFileNameWithoutExtension(pathToFile);
            Extension = Path.GetExtension(pathToFile);
            CreatedAt = DateTime.Now;
            Hash = Convert.ToBase64String(SHA256.Create().ComputeHash(File.ReadAllBytes(pathToFile)));
            IsSigned = false;

        }
    }
}
