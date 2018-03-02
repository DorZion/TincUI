using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tinc.BL;
using Tinc.Communication.Impl;

namespace Tinc.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TincProperties properties = new TincProperties()
            {
                Gateway = IPAddress.Parse("10.8.0.1"),
                Subnet = IPAddress.Parse("10.8.0.2"),
                TincNetName = "vpn",
                TincNodeName = "master",
                NetworkInterfaceName = "Ethernet 2"
            };
            IVpnHost vpnHost = new TincHost(properties);
            IVpnClient vpnClient = new TincClient(properties, "master");

//            vpnHost.Start();
            vpnClient.Connect();
            System.Console.ReadLine();
//            vpnHost.Stop();
            vpnClient.Disconnect();
        }
    }
}
