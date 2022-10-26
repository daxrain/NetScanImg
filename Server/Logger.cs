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
        private StreamWriter log_file;
        private string filename;

        public Logger()
        {
            filename = "log.txt";
        }

        public Logger(string filename)
        {
            this.filename = filename;
        }

        public void write_log(string msg)
        {
            log_file = new StreamWriter(this.filename, append: true);
            string log_msg = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " -> " + msg + Environment.NewLine;
            log_file.Write(log_msg);
            log_file.Close();
        }
    }
}
