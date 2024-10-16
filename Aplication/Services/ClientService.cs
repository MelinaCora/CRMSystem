using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Request;
using Aplication.Responses;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

            var clientEntity = new Clients
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Company = request.Company,
                Address = request.Address,
                CreateDate = DateTime.Now
            };

            await _commands.InsertClient(clientEntity);

            var clientResponse = new CreateClientResponse
            {
                id = clientEntity.ClientID,
                name = clientEntity.Name,
                email = clientEntity.Email,
                phone = clientEntity.Phone,
                company = clientEntity.Company,
                address = clientEntity.Address
            };

            return clientResponse;           
           
        }

        public async Task<List<CreateClientResponse>> GetAll()
        {
            var clients = await _query.GetListClientsAsync();
            return clients.Select(client => new CreateClientResponse
            {
                id = client.ClientID,
                name = client.Name,
                email = client.Email,
                phone = client.Phone,
                company = client.Company,
                address = client.Address
              
            }).ToList();
        }

    }
}