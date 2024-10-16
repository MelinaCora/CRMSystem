using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Request
{
    public class CreateInteractionRequest
    {
        public string notes { get; set; }       
        public DateTime date { get; set; }
        public int InteractionType { get; set; }
    }
}
