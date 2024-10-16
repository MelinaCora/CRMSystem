using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Responses
{
    public class TaskResponse
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public DateTime dueDate { get; set; }
        public Guid projectId { get; set; }
        public CreateTaskStatusResponse status { get; set; }
        public CreateUsersResponse userAssigned { get; set; }
        

    }

    public class CreateUsersResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class CreateTaskStatusResponse
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
