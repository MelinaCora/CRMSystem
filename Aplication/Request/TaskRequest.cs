using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Request
{
    public class TaskRequest
    {
        public string name { get; set; }
        public DateTime dueDate { get; set; }
        public int user { get; set; }
        public int status { get; set; }
    }
}
