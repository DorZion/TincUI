using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tinc.BL
{
    public interface IClientRepository
    {
        void AddClient(Client client);

        void RemoveClient(string id);

        void RemoveClient(IPAddress address);

        IPAddress GetAvailableAddress();

        IEnumerable<Client> GetAllClients();

        event Action<Client> OnClientAdded;

        event Action<Client> OnClientRemoved;
    }
}