using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Responses
{
    public class TaskResponse
    {
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
    }
}
