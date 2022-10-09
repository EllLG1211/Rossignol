using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries.Serialized
{
    public abstract class EncryptedEntry
    {
        public readonly string encryptionType;
        public byte[] EncryptedUid { get; set; }
        public byte[] EncryptedLogin { get; set; }
        public byte[] EncryptedPassword { get; set; }
        public byte[] EncryptedApp { get; set; }
        public byte[] EncryptedNote { get; set; }

        protected EncryptedEntry(string encryptionType, byte[] EncryptedUid, byte[] EncryptedLogin, byte[] EncryptedPassword, byte[] EncryptedApp, byte[] EncryptedNote)
        {
            if (encryptionType == null) throw new ArgumentNullException(nameof(encryptionType));
            if (EncryptedUid == null) throw new ArgumentNullException(nameof(EncryptedUid));
            if (EncryptedLogin == null) throw new ArgumentNullException(nameof(EncryptedLogin));
            if (EncryptedPassword == null) throw new ArgumentNullException(nameof(EncryptedPassword));
            if (EncryptedApp == null) throw new ArgumentNullException(nameof(EncryptedApp));
            if (EncryptedNote == null) throw new ArgumentNullException(nameof(EncryptedNote));

            this.encryptionType = encryptionType;
            this.EncryptedUid = EncryptedUid;
            this.EncryptedLogin = EncryptedLogin;
            this.EncryptedPassword = EncryptedPassword;
            this.EncryptedApp = EncryptedApp;
            this.EncryptedNote = EncryptedNote;
        }
    }
}