using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries.Serialized
{
    public class EncryptedSharedEntry : EncryptedEntry
    {
        public EncryptedSharedEntry(string encryptionType, byte[] EncryptedUid, byte[] EncryptedLogin, byte[] EncryptedPassword, byte[] EncryptedApp, byte[] EncryptedNote)
            : base(encryptionType, EncryptedUid, EncryptedLogin, EncryptedPassword, EncryptedApp, EncryptedNote) { }
    }
}
