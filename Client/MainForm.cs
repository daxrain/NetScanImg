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
using System.Collections.Generic;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Properties;
using System.Drawing.Imaging;

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
        private static List<System.Drawing.Image> images = new List<System.Drawing.Image>();
        private static int list_images_selected_index = 0;
        public MainForm()
        {
            InitializeComponent();
            if (File.Exists("param.xml"))
            {
                param = Parameters.readFile("param.xml");
            }
            else
            {
                param = new Parameters();
                Parameters.SaveFile(param, "param.xml");
            }

            //Set graphical elements
            dpi_comboBox.SelectedIndex = 0;
            color_comboBox.SelectedIndex = 0;
            nextImage_button.Enabled = false;
            deleteImage_button.Enabled = false;
            previousImage_button.Enabled = false;
            default_save_path_textBox.Text = param.default_save_path;

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
                            System.Drawing.Image scanned_img = ((ScanResponse)resp).img;
                            if (scanned_img != null)
                            {
                                images.Add(scanned_img);
                                list_images_selected_index = images.Count - 1;
                                scanned_images_PictureBox.Image = scanned_img;
                                if (list_images_selected_index != 0)
                                {
                                    previousImage_button.Enabled = true;
                                    deleteImage_button.Enabled = true;
                                }
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
            device.Options.brightness = brightness_TrackBar.Value;
            device.Options.contrast = contrast_TrackBar.Value;
            
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
            if (list_images_selected_index > 0)
            {
                images.RemoveAt(list_images_selected_index);
                if(list_images_selected_index == images.Count)
                    list_images_selected_index--;
                scanned_images_PictureBox.Image = images[list_images_selected_index];
            }
            else if (list_images_selected_index == 0)
            {
                if (images.Count == 1)
                {
                    images.RemoveAt(list_images_selected_index);
                    scanned_images_PictureBox.Image = null;
                }
                else if (images.Count != 0)
                {
                    images.RemoveAt(list_images_selected_index);
                    scanned_images_PictureBox.Image = images[list_images_selected_index];
                }   
            }
        }

        private void previousImage_button_Click(object sender, EventArgs e)
        {
            if(list_images_selected_index>0 && list_images_selected_index<images.Count)
            {
                list_images_selected_index--;
                scanned_images_PictureBox.Image=images[list_images_selected_index];
                nextImage_button.Enabled = true;
            }
        }

        private void nextImage_button_Click(object sender, EventArgs e)
        {
            if (list_images_selected_index >= 0 && list_images_selected_index < images.Count)
            {
                list_images_selected_index++;
                if(list_images_selected_index != images.Count)
                    scanned_images_PictureBox.Image = images[list_images_selected_index];
                else
                {
                    list_images_selected_index = images.Count - 1;
                    scanned_images_PictureBox.Image = images[list_images_selected_index];
                }
            }
        }

        private void open_path_button_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Apri percorso salvataggio di default";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                param.default_save_path=folderBrowserDialog.SelectedPath;
                default_save_path_textBox.Text = param.default_save_path;
            }
        }

        private void save_pdf_button_Click(object sender, EventArgs e)
        {
            if (images.Count > 0)
            {
                string filename = param.default_save_path + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmssff") + ".pdf";
                create_pdf(filename);
            }
        }

        private void create_pdf(string filename)
        {
            try
            {
                PdfWriter writer = new PdfWriter(filename);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                foreach (System.Drawing.Image img in images)
                {
                    ImageData img_Data = ImageDataFactory.Create(ImageToByteArray(img));
                    iText.Layout.Element.Image itext_img = new iText.Layout.Element.Image(img_Data);
                    itext_img.SetTextAlignment(TextAlignment.CENTER);
                    document.Add(itext_img);
                }

                document.Close();
                MessageBox.Show("Documento salvato in:\n" + filename);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("Impossibile trovare la cartella per i salvataggi di default:\n " + param.default_save_path);
            }
        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        private void save_image_button_Click(object sender, EventArgs e)
        {
            if(images.Count > 0)
            {
                string filename = param.default_save_path + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmssff") + ".jpg";
                create_jpg(filename);
            }
        }

        private void create_jpg(string filename)
        {
            try
            {
                images[list_images_selected_index].Save(filename, ImageFormat.Jpeg);
                MessageBox.Show("Documento salvato in:\n" + filename);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("Impossibile trovare la cartella per i salvataggi di default:\n " + param.default_save_path);
            }
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            images.Clear();
            list_images_selected_index = 0;
            scanned_images_PictureBox.Image = null;
            nextImage_button.Enabled = false;
            previousImage_button.Enabled =false;
            deleteImage_button.Enabled = false;
        }

        private void saveas_button_Click(object sender, EventArgs e)
        {
            if (images.Count > 0)
            {
                saveFileDialog.Title = "Salva immagine con nome";
                saveFileDialog.Filter = "File (*.jpg;*.pdf)|*.jpg;*.pdf|Tutti (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog.FileName;
                    string ext = Path.GetExtension(filename);

                    try
                    {
                        switch (ext)
                        {
                            case ".pdf":
                                create_pdf(filename);
                                break;
                            case ".jpg":
                                create_jpg(filename);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Impossibile salvare il file:\n" + filename);
                    }
                }
            }
        }
    }
}
