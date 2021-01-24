using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AkkaHost.Applications.Services;
using AkkaHost.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AkkaHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _clientServices;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientServices clientServices, ILogger<ClientController> logger)
        {
            _clientServices = clientServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Client> Get()
        {
            return await _clientServices.GetById(1);
        }
    }
}
