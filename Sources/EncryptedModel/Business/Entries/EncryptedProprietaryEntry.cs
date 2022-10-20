using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedModel.Business.Entries
{
    public class EncryptedProprietaryEntry : EncryptedEntry{
        public List<MailedUser> SharedWith { get; set; }
        public EncryptedProprietaryEntry(string encryptionType, string Uid, byte[] EncryptedLogin, byte[] EncryptedPassword, byte[] EncryptedApp, byte[] EncryptedNote, List<MailedUser> SharedWith) 
            : base(encryptionType, Uid, EncryptedLogin, EncryptedPassword, EncryptedApp, EncryptedNote)
        {
            this.SharedWith = SharedWith;
        }
    }
}
