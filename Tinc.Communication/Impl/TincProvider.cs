using System;
using Tinc.BL;

namespace Tinc.Communication.Impl
{
    public class TincProvider : IVpnProvider
    {
        public IVpnClient GetClient()
        {
            throw new NotImplementedException();
        }

        public IVpnHost GetHost()
        {
            throw new NotImplementedException();
        }
    }
}