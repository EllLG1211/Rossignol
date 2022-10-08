using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries.Serialized
{
    public abstract class EncryptedEntry
    {
        public readonly string encryptionType = "AES";
        public byte[] EncryptedUid { get; set; }
        public byte[] EncryptedLogin { get; set; }
        public byte[] EncryptedPassword { get; set; }
        public byte[] EncryptedApp { get; set; }
        public byte[] EncryptedNote { get; set; }
    }
}