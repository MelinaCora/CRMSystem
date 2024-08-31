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
    public class InteractionTypeQuery : IInteractionTypeQuery
    {
        private readonly CrmContext _context;

        public InteractionTypeQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<InteractionTypes>> GetAllInteractions()
        {
            return await _context.InteractionTypes.ToListAsync();
        }
    }
}
