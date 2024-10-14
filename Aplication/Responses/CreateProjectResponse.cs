using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Responses
{
    public  class CreateProjectResponse
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public CreateCampaignTypesResponse campaignType { get; set; } 
        public CreateClientResponse client { get; set; }
   
    }

    public class CreateCampaignTypesResponse
    {
        public int id { get; set; }
        public string name { get; set; }
    }
      
    
}
