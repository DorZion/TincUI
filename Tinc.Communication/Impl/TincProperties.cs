using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tinc.Communication.Impl
{
    public class TincProperties
    {
        public string NetworkInterfaceName { get; set; }
        public string TincNetName { get; set; }
        public string TincNodeName { get; set; }
        public IPAddress Subnet { get; set; }
        public IPAddress Gateway { get; set; }
    }
}
