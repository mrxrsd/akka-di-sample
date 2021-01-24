using Akka.Actor;
using Akka.DependencyInjection;
using AkkaHost.Domain.Messages;

namespace AkkaHost.Domain.Actors
{
    public class ClientActor : ReceiveActor
    {        
        public ClientActor()
        {        
            Receive<GetClientById>(GetClientByIdHandler);
        }

        private void GetClientByIdHandler(GetClientById input)
        {
            var store = Context.ActorOf(ServiceProvider.For(Context.System).Props<ClientActorStore>());
            store.Forward(input);
        }
    }
}
