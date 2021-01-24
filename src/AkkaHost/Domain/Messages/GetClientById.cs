using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AkkaHost.Domain.Model;

namespace AkkaHost.Domain.Messages
{
    public class GetClientById
    {
        public int Id { get; }

        public GetClientById(int id)
        {
            Id = id;
        }
    }

    public class ClientResult
    {
        public Client Client { get; }

        public ClientResult(Client client)
        {
            Client = client;
        }
    }
}
