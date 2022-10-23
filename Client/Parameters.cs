using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Client
{
    [Serializable]
    public class Parameters
    {
        public string server_addr;
        public int server_port;

        public Parameters()
        {
            server_addr = "127.0.0.1";
            server_port = 9050;
        }

        public Parameters(string server_ip, int port)
        {
            server_addr = server_ip;
            server_port = port;
        }

        public static Parameters readFile(string filename)
        {
            Parameters param;
            XmlSerializer xs = new XmlSerializer(typeof(Parameters));
            FileStream fs = new FileStream(filename, FileMode.Open);
            param = (Parameters)xs.Deserialize(fs);
            fs.Close();
            return param;
        }

        public static void SaveFile(Parameters param, string filename)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Parameters));
            TextWriter txtWriter = new StreamWriter("param.xml");
            xs.Serialize(txtWriter, param);
            txtWriter.Close();
        }
    }
}
