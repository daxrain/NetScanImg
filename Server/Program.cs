using Protocol;
using Protocol.MyMessages;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace Server
{
    public class NetScanImageServer
    {
        public static TcpListener _server;
        public static bool goingOn = true;
        public static Parameters param;
        public static Logger txt_logger;

        static void Main(string[] args)
        {
            init_server();
            try
            {
                Thread th = startListener(param.default_port);
                int res = 0;
                do
                {
                    Console.WriteLine("PRESS 1 INVIO FOR EXIT...");
                    try
                    {
                        res = int.Parse(Console.ReadLine());
                    }
                    catch (System.FormatException ex)
                    {
                        //Console.WriteLine("PRESS 1 INVIO FOR EXIT...");
                    }
                }
                while (res != 1);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                txt_logger.write_log(ex.ToString());
            }
            close_server();
            Console.ReadLine();
        }

        public static void init_server()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Parameters));
            if (File.Exists("param.xml"))
            {
                FileStream fs = new FileStream("param.xml", FileMode.Open);
                param = (Parameters)xs.Deserialize(fs);
                fs.Close();
            }
            else
            {
                param = new Parameters();
                TextWriter txtWriter = new StreamWriter("param.xml");
                xs.Serialize(txtWriter, param);
                txtWriter.Close();
            }

            txt_logger = new Logger(param.filename);
        }

        public static void close_server()
        {
            goingOn = false;
            _server.Stop();
            //th.Interrupt();
        }

        public static Thread startListener(int port)
        {
            Console.WriteLine("Server in ascolto nella porta: " + port);
            txt_logger.write_log("Server in ascolto nella porta: " + port);
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();
            
            Thread th = new Thread(doListen);
            th.Start();
            return th;
        }

        public static void doListen()
        {
            do
            {
                try
                {
                    TcpClient newClient = _server.AcceptTcpClient();
                    IFormatter formatter = new BinaryFormatter();
                    Request req_msg = (Request)formatter.Deserialize(newClient.GetStream());
                    IPEndPoint remote_ipep = (IPEndPoint)newClient.Client.RemoteEndPoint;
                    send_response(remote_ipep, req_msg);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Chiusura imprevista Socket");
                    txt_logger.write_log("Chiusura imprevista Socket");
                }
                    
            }
            while (goingOn);
        }

        //The client listen for responses in default_port + 1
        public static void send_response(IPEndPoint remote_client, Request req_msg)
        {
            remote_client.Port = param.default_port + 1;
            TcpClient _tcpClient = new TcpClient();
            IFormatter formatter = new BinaryFormatter();
            try
            {
                switch (req_msg.MyId)
                {
                    case request_type.EMPTY:
                        _tcpClient.Connect(remote_client);
                        formatter.Serialize(_tcpClient.GetStream(), new EmptyResponse());
                        _tcpClient.Close();
                        Console.WriteLine("INVIO MESSAGGIO VUOTO");
                        txt_logger.write_log("INVIO MESSAGGIO VUOTO");
                        break;
                    case request_type.LIST:
                        _tcpClient.Connect(remote_client);
                        formatter.Serialize(_tcpClient.GetStream(), new ListResponse(Scanner.scanner_list()));
                        _tcpClient.Close();
                        Console.WriteLine("INVIO MESSAGGIO LIST");
                        txt_logger.write_log("INVIO MESSAGGIO LIST");
                        break;
                    case request_type.SCAN:
                        Image scannedImage = null;
                        Request scan_req = req_msg;

                        if (Scanner.scanner_list().Count > 0 && !String.IsNullOrEmpty(scan_req.Device.ID) )
                        {
                            try
                            {
                                /*Thread thread = new Thread(
                                    () =>
                                        scannedImage = Scanner.scan(Scanner.getDevInfo(scan_req.Device.ID), scan_req.Device.Options)
                                    );
                                thread.Start();
                                thread.Join();*/
                                scannedImage = Scanner.scan(Scanner.getDevInfo(scan_req.Device.ID), scan_req.Device.Options);
                                _tcpClient.Connect(remote_client);
                                //formatter.Serialize(_tcpClient.GetStream(), new ScanResponse(Image.FromFile(@"C:\porcini.jpg")));
                                formatter.Serialize(_tcpClient.GetStream(), new ScanResponse(scannedImage));
                                _tcpClient.Close();
                                //thread.Interrupt();
                                Console.WriteLine("INVIO IMMAGINE");
                                txt_logger.write_log("INVIO IMMAGINE");
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                //_tcpClient.Connect(remote_client);
                                //formatter.Serialize(_tcpClient.GetStream(), new ScanErrorResponse(ex.ToString()));
                                //_tcpClient.Close();
                            }
                        }
                        break;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("IMPOSSIBILE CONNETTERSI\n" + exc.ToString());
                txt_logger.write_log("IMPOSSIBILE CONNETTERSI\n" + exc.ToString());
            }
        }
    }
}
