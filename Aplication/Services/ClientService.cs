using Aplication.Interfaces;
using Aplication.Request;
using Aplication.Responses;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class ClientService : IClientServices
    {

        private readonly IClientsCommand _commands;
        private readonly IClientQuery _query;

        public ClientService(IClientsCommand commands, IClientQuery query)
        {
            _commands = commands;
            _query = query;
        }
        public async Task<CreateClientResponse> CreateClient(ClientRequest request)
        {
            Clients Client = new Clients()
            {
                Name = request.Name,
                Email = request.Email,
                Company = request.Company,
                Phone = request.Phone,
                Address = request.Address,
            };

            await _commands.InsertClient(Client);
            return new CreateClientResponse
            {

                Name = Client.Name,
                Email = Client.Email,
                Company = Client.Company,
                Phone = Client.Phone,
                Address = Client.Address,

            };
        }

        public async Task<List<Clients>> GetAll()
        {

            List<Clients> clients = new List<Clients>();
            clients = await _query.GetListClientsAsync();
            return clients;
        }

    }
}
