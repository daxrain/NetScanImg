using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Runtime.Serialization;
using Protocol.MyMessages;
using Protocol;
using System.IO;
using System.Collections;

namespace Client
{
    public partial class MainForm : Form
    {
        public static IPAddress server_ip;
        private static int default_port = 9050;
        public static TcpListener _response_listener;
        public static bool goingOn = true;
        public BinaryFormatter bf = new BinaryFormatter();
        public static Parameters param;
        
        public MainForm()
        {
            InitializeComponent();

            dpi_comboBox.SelectedIndex = 0;
            color_comboBox.SelectedIndex = 0;

            if (File.Exists("param.xml"))
            {
                param = Parameters.readFile("param.xml");
            }
            else
            {
                param = new Parameters();
                Parameters.SaveFile(param, "param.xml");
            }

            server_ip = IPAddress.Parse(param.server_addr);
            default_port = param.server_port;
            
            server_ip_TextBox.Text = param.server_addr;
            server_port_TextBox.Text = param.server_port.ToString();

            startListener(default_port + 1);
            send_request(new Request(request_type.LIST));
        }
        
        public Thread startListener(int port)
        {
            _response_listener = new TcpListener(IPAddress.Any, port);
            _response_listener.Start();

            Thread th = new Thread(doListen);
            th.Start();
            return th;
        }

        public void doListen()
        {
            do
            {
                try
                {
                    TcpClient newClient = _response_listener.AcceptTcpClient();
                    IFormatter formatter = new BinaryFormatter();
                    Response resp = (Response)formatter.Deserialize(newClient.GetStream());
                    if(resp is ListResponse)
                    {
                        ListResponse resp_list = (ListResponse)resp;
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            if (resp_list.scanners.Count == 0)
                            {
                                resp_list.scanners.Add(new Scanner("Nessuno", "Nessuno"));
                                btn_scan.Enabled = false;
                            }
                            foreach(Scanner dev in resp_list.scanners)
                                scanner_comboBox.Items.Add(dev);
                            //scanner_comboBox.DataSource = resp_list.scanners;
                            scanner_comboBox.ValueMember = "ID";
                            scanner_comboBox.DisplayMember = "Name";
                            scanner_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                            scanner_comboBox.SelectedIndex = 0;
                        });
                        
                        //resp_list.scanners
                        //MessageBox.Show("HO RICEVUTO LA LISTA DEGLI SCANNER");
                    }
                    else if(resp is ScanResponse)
                    {
                        this.InvokeEx(f => f.testImmagine.Image = ((ScanResponse)resp).img);
                    }
                    else if(resp is EmptyResponse)
                    {
                        MessageBox.Show("Connessione avvenuta con successo");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Chiusura imprevista Socket");
                }

            }
            while (goingOn);
        }

        
        private void btn_test_connection_Click(object sender, EventArgs e)
        {
            param.server_addr = server_ip_TextBox.Text;
            param.server_port = int.Parse(server_port_TextBox.Text);
            server_ip = IPAddress.Parse(param.server_addr);
            default_port = param.server_port;
            goingOn = false;
            _response_listener.Stop();
            Thread.Sleep(1000);
            goingOn = true;
            startListener(default_port + 1);
            send_request(new Request(request_type.EMPTY));
        }

        private void btn_scan_Click(object sender, EventArgs e)
        {
            Scanner device = (Scanner)scanner_comboBox.SelectedItem;
            device.Options = new ScannerOptions();
            device.Options.dpi = int.Parse((string)dpi_comboBox.SelectedItem);
            device.Options.color_mode = color_comboBox.SelectedIndex;
            device.Options.brightness = 0;
            device.Options.contrast = 0;
            
            send_request(new Request(request_type.SCAN, device));
        }

        private void send_request(Request req)
        {
            IPEndPoint ipep = new IPEndPoint(server_ip, default_port);
            TcpClient _tcpClient = new TcpClient();
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                _tcpClient.Connect(ipep);
                formatter.Serialize(_tcpClient.GetStream(), req);
            }
            catch (Exception exc)
            {
                MessageBox.Show("IMPOSSIBILE CONNETTERSI\n");
            }
            _tcpClient.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            goingOn = false;
            _response_listener.Stop();
        }

        private void btn_save_conf_Click(object sender, EventArgs e)
        {
            param.server_addr = server_ip_TextBox.Text;
            param.server_port = int.Parse(server_port_TextBox.Text);
            server_ip = IPAddress.Parse(param.server_addr);
            default_port = param.server_port;
            Parameters.SaveFile(param, "param.xml");
            goingOn = false;
            _response_listener.Stop();
            Thread.Sleep(1000);
            goingOn=true;
            startListener(default_port + 1);
        }
    }
}
