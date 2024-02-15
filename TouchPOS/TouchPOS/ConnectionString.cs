using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace TouchPOS
{
    class ConnectionString
    {
        string cs = "";
        string st = "";
        public String ReadCS() 
        {
            using (StreamReader sr = new StreamReader("SQLSettings.dat")) 
            {
                st = sr.ReadLine();
            }
            return st;
        }
    }
}
