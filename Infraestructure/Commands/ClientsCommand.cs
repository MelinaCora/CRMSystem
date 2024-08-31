using Aplication.Interfaces;
using CRMSystem.Data;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Commands
{
    public class ClientsCommand : IClientsCommand
    {
        private readonly CrmContext _context;

        public ClientsCommand(CrmContext context)
        {
            _context = context;
        }

        public async Task InsertClient(Clients client)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
        }
    }
}
