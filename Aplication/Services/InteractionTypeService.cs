using Aplication.Interfaces;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class InteractionTypeService : IInteractionTypeService
    {
        private readonly IInteractionTypeQuery _query;

        public InteractionTypeService(IInteractionTypeQuery query)
        {
            _query = query;
        }

        public async Task<List<InteractionTypes>> GetAllInteractions()
        {
            List<InteractionTypes> interactions = new List<InteractionTypes>();
            interactions = await _query.GetAllInteractions();
            return interactions;
        }
    }
}
