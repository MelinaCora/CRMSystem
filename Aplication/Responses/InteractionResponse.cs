using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Responses
{
    public class InteractionResponse
    {
        public Guid id { get; set; }
        public string notes { get; set; }
        public DateTime date { get; set; }
        public Guid projectId { get; set; }
        public CreateInteractionTypeResponse interactionType { get; set; }
      
    }

    public class CreateInteractionTypeResponse
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
