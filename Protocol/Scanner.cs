using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    [Serializable]
    public class Scanner
    {
        public string ID;
        public string Name;

        public Scanner(string ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
