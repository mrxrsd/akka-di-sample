using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaHost.Domain.Messages;
using AkkaHost.Domain.Model;

namespace AkkaHost.Applications.Services
{
    public interface IClientServices
    {
        Task<Client> GetById(int id);

    }

    public class ClientServices : IClientServices
    {
        private readonly IActorRef _clientActor;

        public ClientServices(IActorRef clientActor)
        {
            _clientActor = clientActor;
        }

        public async Task<Client> GetById(int id)
        {
            var result = await _clientActor.Ask<ClientResult>(new GetClientById(id), new TimeSpan(0, 0, 0, 3));

            return result?.Client;
        }
    }
}
