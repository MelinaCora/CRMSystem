using Microsoft.VisualBasic;

namespace CRMSystem.Models
{
    public class Projects
    {
        public Guid ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public int CampaignType { get; set; }    // Foreign Key
        public CampaignTypes CampaignTypes { get; set; }

        public int ClientID { get; set; }          // Foreign Key

        public Clients Clients { get; set; }    

        public List<Tasks> TaskStatus { get; set; }
        public List<Interactions> Interaction { get; set; }
    }
}
