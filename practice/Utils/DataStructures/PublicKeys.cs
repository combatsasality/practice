using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.Utils.DataStructures
{
    public struct PublicKeys
    {
        public Guid Id { get; set; }
        public Guid OwnerUuid { get; set; }
        public string PublicKey { get; set; }
        public DateTime CreatedAt { get; set; }

        public PublicKeys(Guid ownerUuid, string publicKey)
        {
            Id = Guid.NewGuid();
            OwnerUuid = ownerUuid;
            PublicKey = publicKey;
            CreatedAt = DateTime.Now;
        }
    }
}
