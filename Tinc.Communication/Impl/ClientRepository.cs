using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;

namespace Tinc.BL
{
    public class ClientRepository : IClientRepository
    {
        private readonly List<Client> mClients;

        public ClientRepository()
        {
            mClients = new List<Client>();
        }

        public void AddClient(Client client)
        {
            mClients.Add(client);

            OnClientAdded?.Invoke(client);
        }

        public void RemoveClient(string id)
        {
            Client client = mClients.Find(c => c.Id == id);
            if (client != null)
            {
                mClients.Remove(client);
                OnClientRemoved?.Invoke(client);
            }
        }

        public void RemoveClient(IPAddress address)
        {
            Client client = mClients.Find(c => c.Address.Equals(address));
            if (client != null)
            {
                mClients.Remove(client);
                OnClientRemoved?.Invoke(client);
            }
        }

        public IPAddress GetAvailableAddress()
        {
            return IPAddress.Parse("10.8.0.2");
        }

        public IEnumerable<Client> GetAllClients()
        {
            return mClients;
        }

        public event Action<Client> OnClientAdded;

        public event Action<Client> OnClientRemoved;
    }
}