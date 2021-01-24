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
            // mixin  DI Args and static args! 
            var store = Context.ActorOf(ServiceProvider.For(Context.System).Props<ClientActorStore>("arg1","arg2"));
            store.Forward(input);
        }
    }
}
