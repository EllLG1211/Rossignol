using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public interface IEncrypter
    {
        byte[] Encrypt(string key, string toEncrypt);
    }
}
