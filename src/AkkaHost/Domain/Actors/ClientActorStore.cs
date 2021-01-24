using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaHost.Domain.Messages;
using AkkaHost.Domain.Repositories;

namespace AkkaHost.Domain.Actors
{
    public class ClientActorStore : ReceiveActor
    {
        private readonly Func<IClientRepository> _clientRepositoryFactory;

        public ClientActorStore(Func<IClientRepository> clientRepositoryFactory)
        {
            _clientRepositoryFactory = clientRepositoryFactory;

            ReceiveAsync<GetClientById>(GetClientByIdHandler);
        }

        private async Task GetClientByIdHandler(GetClientById input)
        {
            var repository = _clientRepositoryFactory();

            var result = await repository.GetById(input.Id);

            Sender.Tell(new ClientResult(result));
        }
    }
}
