
using Aplication.Pagination;
using Aplication.Request;
using Aplication.Responses;
using CRMSystem.Models;
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
        Task<PagedResult<Projects>> GetProjectsAsync(string projectName = null,
            int? campaignTypeId = null,
            int? clientId = null,
            int pageNumber = 1,
            int pageSize = 10);

        Task<ProjectDetailsResponse> GetProjectByIdAsync(Guid projectId);
        Task<bool> AddInteractionAsync(Guid projectId,CreateInteractionRequest request);
        Task <bool> AddTaskToProject(Guid projectId, TaskRequest request);
        Task<TaskResponse> UpdateTaskAsync(Guid taskId, TaskRequest request);
    }
}
