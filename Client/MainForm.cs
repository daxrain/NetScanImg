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
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace Client
{
    public partial class MainForm : Form
    {
        private static IPAddress server_ip;
        private static int default_port = 9050;
        private static TcpListener _response_listener;
        private static bool goingOn = true;
        private BinaryFormatter bf = new BinaryFormatter();
        private static Parameters param;
        private static List<Image> images = new List<Image>();
        private static int list_images_selected_index = 0;
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
                            //scanner_comboBox.DataSource = resp_list.scanners; //Non funziona
                            scanner_comboBox.ValueMember = "ID";
                            scanner_comboBox.DisplayMember = "Name";
                            scanner_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                            scanner_comboBox.SelectedIndex = 0;
                        });
                    }
                    else if(resp is ScanResponse)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            Image scanned_img = ((ScanResponse)resp).img;
                            if (scanned_img != null)
                            {
                                images.Add(scanned_img);
                                list_images_selected_index = images.Count - 1;
                                scanned_images_PictureBox.Image = scanned_img;
                            }
                        });
                        //this.InvokeEx(f => f.scanned_images_PictureBox.Image = ((ScanResponse)resp).img);
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

        private void deleteImage_button_Click(object sender, EventArgs e)
        {
            if(images.Count != 0)
                images.RemoveAt(list_images_selected_index);

            //Cancello in testa
            if (list_images_selected_index < images.Count)
            {
                images.RemoveAt(list_images_selected_index);
                if (list_images_selected_index == images.Count)
                {
                    scanned_images_PictureBox.Image = images[images.Count - 1];
                    list_images_selected_index--;
                }
                else if (list_images_selected_index < images.Count && list_images_selected_index != 0)
                {
                    scanned_images_PictureBox.Image = images[list_images_selected_index];
                }
                else if (list_images_selected_index == 0)
                {
                    scanned_images_PictureBox = null;
                }
            }
        }

        private void previousImage_button_Click(object sender, EventArgs e)
        {
            if(list_images_selected_index>0 && list_images_selected_index<images.Count)
            {
                list_images_selected_index--;
                scanned_images_PictureBox.Image=images[list_images_selected_index];
            }
        }

        private void nextImage_button_Click(object sender, EventArgs e)
        {
            if (list_images_selected_index > 0 && list_images_selected_index < images.Count)
            {
                list_images_selected_index++;
                scanned_images_PictureBox.Image = images[list_images_selected_index];
            }
        }
    }
}
