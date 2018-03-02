using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Open.Nat;
using Tinc.BL;
using Tinc.Communication.Properties;

namespace Tinc.Communication.Management.WebAPI
{
    public class ClientManagementOwin : IManagmentServerHost
    {
        private IDisposable mWebApp;

        public void Start()
        {
            UpnpUtils.ForwardPort(Protocol.Tcp, Settings.Default.ManagementPort, Settings.Default.ManagementPort,
                "Tinc Client Management");
            mWebApp = WebApp.Start<ClientManagmentOwinStartup>($"http://localhost:{Settings.Default.ManagementPort}");
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            mWebApp?.Dispose();
            mWebApp = null;
        }
    }
}