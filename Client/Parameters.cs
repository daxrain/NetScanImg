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
        public string default_save_path;

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
            Parameters param = null;
            string app_data_roaming_directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string netscanimg_dir_file = Path.Combine(app_data_roaming_directory, "NetScanImg",filename);
            XmlSerializer xs = new XmlSerializer(typeof(Parameters));

            if (File.Exists(netscanimg_dir_file))
            {
                
                FileStream fs = new FileStream(netscanimg_dir_file, FileMode.Open);
                param = (Parameters)xs.Deserialize(fs);
                fs.Close();
            }
            return param;
        }

        public static void SaveFile(Parameters param, string filename)
        {
            string app_data_roaming_directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string netscanimg_dir = Path.Combine(app_data_roaming_directory, "NetScanImg");
            XmlSerializer xs = new XmlSerializer(typeof(Parameters));
            if (!Directory.Exists(netscanimg_dir))
                Directory.CreateDirectory(netscanimg_dir);

            TextWriter txtWriter = new StreamWriter(Path.Combine(netscanimg_dir, filename));
            xs.Serialize(txtWriter, param);
            txtWriter.Close();
        }
    }
}
