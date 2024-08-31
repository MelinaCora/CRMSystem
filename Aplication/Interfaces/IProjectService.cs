using Aplication.Request;
using Aplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IProjectService
    {
        Task<CreateProjectResponse> CreateProject(ProjectRequest project);
    }
}
