using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tinc.BL
{
    public interface IVpnClient : IDisposable
    {
        void Connect();

        void Disconnect();
    }
}
