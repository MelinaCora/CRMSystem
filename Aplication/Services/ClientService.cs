using Aplication.Exceptions;
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

        public async Task<Clients> CreateClient(ClientRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
               throw new RequiredParameterException("Error. Client Name is required");
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                throw new RequiredParameterException("Error. Client Email is required");
            }

            if (string.IsNullOrEmpty(request.Phone))
            {
                throw new RequiredParameterException("Error. Client Phone is required");
            }

            if (string.IsNullOrEmpty(request.Company))
            {
                throw new RequiredParameterException("Error. Client Company is required");
            }

            if (string.IsNullOrEmpty(request.Address))
            {
                throw new RequiredParameterException("Error. Client Adress is required");
            }

            Clients Client = new Clients()
                {
                    Name = request.Name,
                    Email = request.Email,                    
                    Phone = request.Phone,
                    Company = request.Company,
                    Address = request.Address,
                };

                await _commands.InsertClient(Client);
                return new Clients
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