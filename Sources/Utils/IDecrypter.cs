﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public interface IDecrypter
    {
        string Decrypt(string key, Entry entry);
    }
}
