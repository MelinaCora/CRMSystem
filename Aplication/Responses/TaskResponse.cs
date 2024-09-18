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
        public CreateUsersResponse Users { get; set; }
        public CreateTaskStatusResponse TaskStatus { get; set; }

    }

    public class CreateUsersResponse
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CreateTaskStatusResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
