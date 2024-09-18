using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Responses
{
    public class ProjectDetailsResponse
    {
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CreateCampaignTypesResponse CampaignTypes { get; set; }
        public CreateClientResponse Clients { get; set; }
        public List<InteractionResponse> Interactions { get; set; }
        public List<TaskResponse> Tasks { get; set; }

    }
}
