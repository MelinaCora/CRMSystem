using Aplication.Interfaces;
using Aplication.Responses;
using CRMSystem.Data;
using CRMSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Querys
{
    public class ClientQuery : IClientQuery
    {
        private readonly CrmContext _context;

        public ClientQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<Clients>> GetListClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Clients> GetClientByIdAsync(int clientId)
        {
            // Buscar el cliente por ID en la base de datos
            var client = await _context.Clients
                                       .FirstOrDefaultAsync(c => c.ClientID == clientId);

            return client;
        }
    }
}

