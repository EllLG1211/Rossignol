using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class TermReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public int ReadInt()
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception e)
            {
                return -1;
            }
        }
    }
}
