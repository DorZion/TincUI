using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tinc.BL
{
    public class Client
    {
        public String Id { get; set; }

        public IPAddress Address { get; set; }

        public String Nickname { get; set; }
    }
}