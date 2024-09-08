using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Request
{
    public class ProjectRequest
    {
        public string ProjectReqName { get; set; }
        public DateTime reqStartDate { get; set; }
        public DateTime reqEndDate { get; set; }
        public int reqCampaignID { get; set; }
        public string reqClientName { get; set; }
        public string reqClientEmail { get; set; }
        public string reqClientPhone { get; set; }
        public string reqClientCompany { get; set; }
        public string reqClientAddress { get; set; }

    }

}
