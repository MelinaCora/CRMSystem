using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Responses
{
    public class InteractionResponse
    {
        public Guid InteractionId { get; set; }
        public CreateInteractionTypeResponse InteractionType { get; set; }
        public string Notes { get; set; }
        
    }

    public class CreateInteractionTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
