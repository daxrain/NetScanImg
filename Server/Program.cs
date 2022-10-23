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

namespace Server
{
    public class NetScanImageServer
    {
        public static TcpListener _server;
        public static bool goingOn = true;
        public static Parameters param;
        static void Main(string[] args)
        {
            init();
            Thread th = startListener(param.default_port); 
            int res = 0;
            do
            {
                Console.WriteLine("PRESS 1 INVIO FOR EXIT...");
                try
                {
                    res = int.Parse(Console.ReadLine());
                }
                catch(System.FormatException ex)
                {
                    //Console.WriteLine("PRESS 1 INVIO FOR EXIT...");
                }
            }
            while (res != 1);
            goingOn = false;
            _server.Stop();
            //th.Interrupt();
            Console.ReadLine();
        }

        public static void init()
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
        }

        public static Thread startListener(int port)
        {
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
                        Console.WriteLine("MESSAGGIO VUOTO");
                        break;
                    case request_type.SCAN:
                        
                        Image scannedImage = Scanner.scan();

                        _tcpClient.Connect(remote_client);
                        //formatter.Serialize(_tcpClient.GetStream(), new ScanResponse(Image.FromFile(@"C:\porcini.jpg")));
                        formatter.Serialize(_tcpClient.GetStream(), new ScanResponse(scannedImage));
                        _tcpClient.Close();
                        Console.WriteLine("INVIO IMMAGINE");
                        break;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("IMPOSSIBILE CONNETTERSI\n" + exc.ToString());
            }
        }
    }
}
