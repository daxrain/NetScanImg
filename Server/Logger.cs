using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Logger
    {
        public StreamWriter log_file;

        public Logger()
        {
            log_file = new StreamWriter("log.txt", append: true);
        }

        public Logger(string filename)
        {
            log_file = new StreamWriter(filename, append: true);
        }

        public void write_log(string msg)
        {
            log_file.Write(msg);
        }
    }
}
