using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinc.BL;

namespace Tinc.Communication
{
    public interface IManagmentServerHost : IDisposable
    {
        void Start();

        void Stop();
    }
}