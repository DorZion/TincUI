using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinc.BL
{
    public interface IVpnProvider
    {
        IVpnClient GetClient();

        IVpnHost GetHost();
    }
}