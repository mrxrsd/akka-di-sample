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
        private readonly string _arg2;
        private readonly string _arg1;

        public ClientActorStore(string arg1, Func<IClientRepository> clientRepositoryFactory, string arg2)
        {
            _arg1 = arg1;
            _clientRepositoryFactory = clientRepositoryFactory;
            _arg2 = arg2;

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
