using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedModel.Business.Entries
{
    public abstract class EncryptedEntry
    {
        public readonly string encryptionType;
        public string Uid { get; set; }
        public byte[] EncryptedLogin { get; set; }
        public byte[] EncryptedPassword { get; set; }
        public byte[] EncryptedApp { get; set; }
        public byte[] EncryptedNote { get; set; }

        protected EncryptedEntry(string encryptionType, string Uid, byte[] EncryptedLogin, byte[] EncryptedPassword, byte[] EncryptedApp, byte[] EncryptedNote)
        {
            if (encryptionType == null) throw new ArgumentNullException(nameof(encryptionType));
            if (Uid == null) throw new ArgumentNullException(nameof(Uid));
            if (EncryptedLogin == null) throw new ArgumentNullException(nameof(EncryptedLogin));
            if (EncryptedPassword == null) throw new ArgumentNullException(nameof(EncryptedPassword));
            if (EncryptedApp == null) throw new ArgumentNullException(nameof(EncryptedApp));
            if (EncryptedNote == null) throw new ArgumentNullException(nameof(EncryptedNote));

            this.encryptionType = encryptionType;
            this.Uid = Uid;
            this.EncryptedLogin = EncryptedLogin;
            this.EncryptedPassword = EncryptedPassword;
            this.EncryptedApp = EncryptedApp;
            this.EncryptedNote = EncryptedNote;
        }
    }
}