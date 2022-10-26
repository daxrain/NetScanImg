using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Parameters
    {
        public int default_port;
        public string filename;

        public Parameters()
        {
            default_port = 9050;
            filename = "log.txt";
        }

        public Parameters(int port)
        {
            default_port = port;
            filename = "log.txt";
        }

        public Parameters(int port, string log_filename)
        {
            default_port = port;
            filename = log_filename;
        }
    }
}
