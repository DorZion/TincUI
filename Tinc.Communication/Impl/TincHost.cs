using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Open.Nat;
using Tinc.BL;
using Tinc.Communication.Properties;

namespace Tinc.Communication.Impl
{
    public class TincHost : BaseTinc, IVpnHost
    {
        public TincHost(TincProperties properties) : base(properties)
        {
        }

        protected override void AdvancedConfigureTinc()
        {
            SetAddress();
        }

        private void SetAddress()
        {
            mTincProcess.StartInfo.Arguments = $"-n {mProperties.TincNetName} add address={UpnpUtils.GetExternalAddress().Result}";
            mTincProcess.Start();
            mTincProcess.WaitForExit();
        }

        public void Start()
        {
            ForwardPort();
            Configure();
            StartDaemon();
        }

        public void Stop()
        {
            Terminate();
        }

        private void ForwardPort()
        {
            UpnpUtils.ForwardPort(Protocol.Udp, 655, 655, "Tinc");
        }
    }
}