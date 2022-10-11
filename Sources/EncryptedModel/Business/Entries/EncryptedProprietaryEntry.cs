﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Business.Entries.Serialized
{
    public class EncryptedProprietaryEntry : EncryptedEntry{
        public byte[] EncryptedSharedWith { get; set; }
        public EncryptedProprietaryEntry(string encryptionType, byte[] EncryptedUid, byte[] EncryptedLogin, byte[] EncryptedPassword, byte[] EncryptedApp, byte[] EncryptedNote, byte[] EncryptedSharedWith) 
            : base(encryptionType, EncryptedUid, EncryptedLogin, EncryptedPassword, EncryptedApp, EncryptedNote)
        {
            this.EncryptedSharedWith = EncryptedSharedWith;
        }
    }
}
