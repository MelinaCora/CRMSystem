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
        public string InteractionType { get; set; }
        public string Notes { get; set; }
    }
}
