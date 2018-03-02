using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Open.Nat;

namespace Tinc.Communication
{
    public static class UpnpUtils
    {
        public static async void ForwardPort(Protocol protocol, short privatePort, short publicPort, string name)
        {
            var discoverer = new NatDiscoverer();
            var cts = new CancellationTokenSource(10000);
            var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);

            await device.CreatePortMapAsync(new Mapping(protocol, privatePort, publicPort, name));
        }

        public static async Task<IPAddress> GetExternalAddress()
        {
            var discoverer = new NatDiscoverer();
            NatDevice device = await discoverer.DiscoverDeviceAsync();
            IPAddress ip = await device.GetExternalIPAsync();

            return ip;
        }
    }
}