using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AkkaHost.Domain.Model;

namespace AkkaHost.Domain.Repositories
{
    public interface IClientRepository
    {
        public Task<Client> GetById(int id);
    }
}
