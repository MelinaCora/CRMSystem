using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Request
{
    public class CreateInteractionRequest
    {
        public Guid ProjectId { get; set; }
        public int InteractionType { get; set; }
        public DateTime InteractionDate { get; set; }
        public string Description { get; set; }
    }
}
