using Aplication.Interfaces;
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

        public Clients GetClient(int ClientID)
        {
            throw new NotImplementedException();
        }

        public async Task <List<Clients>> GetListClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }
    }
}
