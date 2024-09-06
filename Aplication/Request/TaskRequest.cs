using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Request
{
    public class TaskRequest
    {
        public string TaskName { get; set; }
        public DateTime DueDate { get; set; }
        public int AssignedTo { get; set; } // User ID
        public int StatusId { get; set; } // Task status
    }
}
