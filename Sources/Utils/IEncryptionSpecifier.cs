using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption
{
    public interface IEncryptionSpecifier
    {
        /// <summary>
        /// Returns the encryption type of the impelmented algorithm
        /// </summary>
        /// <returns></returns>
        public string EncryptionType();
    }
}
