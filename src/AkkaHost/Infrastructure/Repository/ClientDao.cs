using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AkkaHost.Domain.Model;
using AkkaHost.Domain.Repositories;

namespace AkkaHost.Infrastructure.Repository
{
    public class ClientDao : IClientRepository
    {

        private static readonly List<Client> _user = new List<Client>
        {
            new Client { Id = 1, Name = "Peter"}, 
            new Client { Id = 2, Name = "Bob"}, 
            new Client { Id = 3, Name = "Joseph"}
        };


        public Task<Client> GetById(int id)
        {
            return Task.FromResult(_user.FirstOrDefault(x => x.Id == id));
        }
    }
}
