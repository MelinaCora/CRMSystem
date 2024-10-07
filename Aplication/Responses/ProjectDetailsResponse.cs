using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Responses
{
    public class ProjectDetailsResponse
    {
       public CreateProjectResponse Data { get; set; }
       public List<InteractionResponse> Interactions { get; set; }
       public List<TaskResponse> Tasks { get; set; }

    }
}
