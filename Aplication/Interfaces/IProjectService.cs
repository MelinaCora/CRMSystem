
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
        Task<PagedResult<Projects>> GetProjectsAsync(string Name = null,
            int? campaign = null,
            int? client = null,
            int? offset = null,
            int? size = null);

        Task<ProjectDetailsResponse> GetProjectByIdAsync(Guid projectId);
        Task<bool> AddInteractionAsync(Guid projectId,CreateInteractionRequest request);
        Task <bool> AddTaskToProject(Guid projectId, TaskRequest request);
        Task<TaskResponse> UpdateTaskAsync(Guid taskId, TaskRequest request);
    }
}
