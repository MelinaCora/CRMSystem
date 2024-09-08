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
    public class CampaignTypeQuery : ICampaignTypeQuery
    {
        private readonly CrmContext _context;

        public CampaignTypeQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<CampaignTypes>> GetAllCampaignTypes()
        {
            var campaignTypesList = new List<CampaignTypes>();

            return await _context.CampaignTypes.ToListAsync();
        }

        public async Task<CampaignTypes> GetCampaignTypeByIdAsync(int campaignTypeId)
        {
            return await _context.CampaignTypes.FirstOrDefaultAsync(ct => ct.Id == campaignTypeId);
        }
    }
}
