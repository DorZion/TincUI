using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tinc.BL;

namespace Tinc.Communication.Impl
{
    public class TincClient : BaseTinc, IVpnClient
    {
        private bool mIsConnected;
        private readonly string mRemoteNodeName;

        public TincClient(TincProperties properties, string remoteNodeName) : base(properties)
        {
            mRemoteNodeName = remoteNodeName;
            mIsConnected = false;
        }

        public void Connect()
        {
            if (mIsConnected)
            {
                Terminate();
            }

            Configure();
            StartDaemon();

            mIsConnected = true;
        }

        public void Disconnect()
        {
            Terminate();
            mIsConnected = false;
        }

        protected override void AdvancedConfigureTinc()
        {
            mTincProcess.StartInfo.Arguments =
                $"-n {mProperties.TincNetName} add connectto={mRemoteNodeName}";
            mTincProcess.Start();
            mTincProcess.WaitForExit();
        }
    }
}