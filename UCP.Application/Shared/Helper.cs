using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCP.Application.Shared
{
    public static class Helper
    {
        public static string GenerateAccountNo()
        { 
            Random rnd = new Random();
            string a;
            string b = "ACC-";
            for (int j = 0; j < 1; j++)
            {
                a = rnd.Next().ToString();
                b = $"{a.Substring(0, 9)}".ToString();

            }
            return b;
        }
    }
}
