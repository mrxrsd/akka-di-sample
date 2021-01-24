using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using Akka.DependencyInjection;
using Akka.Pattern;
using AkkaHost.Domain.Actors;
using Microsoft.Extensions.Hosting;

namespace AkkaHost
{
    public class ActorSystemManager : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private ActorSystem _actorSystem;

        public static IActorRef ClientActor { get; set; }

        public ActorSystemManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static class ActorPaths
        {
            public static ActorMetaData ClientActor = new ActorMetaData("client");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var hocon     = ConfigurationLoader.Load();
            var bootstrap = BootstrapSetup.Create().WithConfig(hocon);
            var di        = ServiceProviderSetup.Create(_serviceProvider);
            var actorSystemSetup = bootstrap.And(di);
            _actorSystem = ActorSystem.Create("shop", actorSystemSetup);

            ClientActor = _actorSystem.ActorOf(Props.Create<ClientActor>());

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // theoretically, shouldn't even need this - will be invoked automatically via CLR exit hook
            // but it's good practice to actually terminate IHostedServices when ASP.NET asks you to
            await CoordinatedShutdown.Get(_actorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance);
        }
    }

    public static class ConfigurationLoader
    {
        public static Config Load() => LoadConfig("akka.conf");

        private static Config LoadConfig(string configFile)
        {
            if (File.Exists(configFile))
            {
                var config = File.ReadAllText(configFile);
                return ConfigurationFactory.ParseString(config);
            }
            return Config.Empty;
        }
    }

    public class ActorMetaData
    {
        public ActorMetaData(string name, ActorMetaData parent = null)
        {
            Name = name;
            Parent = parent;
            // if no parent, we assume a top-level actor
            var parentPath = parent != null ? parent.Path : "/user";
            Path = string.Format("{0}/{1}", parentPath, Name);
        }

        public string Name { get; private set; }

        public ActorMetaData Parent { get; set; }

        public string Path { get; private set; }
    }
}
