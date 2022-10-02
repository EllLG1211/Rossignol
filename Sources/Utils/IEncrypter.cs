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
        string Encrypt(string key, Entry entry);
    }
}
