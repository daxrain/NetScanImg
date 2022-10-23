using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Protocol.MyMessages;
using Server;

namespace NetScanImgService
{
    public partial class NetScanImgService : ServiceBase
    {
        
        public NetScanImgService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            NetScanImageServer.init();
            NetScanImageServer.startListener(NetScanImageServer.param.default_port);
        }

        protected override void OnStop()
        {
            NetScanImageServer.goingOn = false;
            NetScanImageServer._server.Stop();
        }
    }
}
