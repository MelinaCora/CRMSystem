using Aplication.Interfaces;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class CampaignTypeService : ICampaignTypeService
    {
        private readonly ICampaignTypeQuery _query;

        public CampaignTypeService(ICampaignTypeQuery query)
        {
            _query = query;
        }

        public async Task<List<CampaignTypes>> GetAll()
        {
            List<CampaignTypes> campaigns = new List<CampaignTypes>();
            campaigns = await _query.GetAllCampaignTypes();
            return campaigns;
        }
    }
}
