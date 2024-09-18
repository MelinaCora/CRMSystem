using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Request
{
    public class ProjectRequest
    {
        //public Guid ProjectID { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int CampaignID { get; set; }
        public int ClientID { get; set; }  
    }

}
