
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
        Task<ProjectDetailsResponse> CreateProject(ProjectRequest project);
        Task<List<CreateProjectResponse>> GetProjectsAsync(string? Name,
            int? campaign,
            int? client,
            int? offset,
            int? limit);

        Task<ProjectDetailsResponse> GetProjectByIdAsync(Guid projectId);
        Task<InteractionResponse> AddInteractionAsync(Guid projectId,CreateInteractionRequest request);
        Task <TaskResponse> AddTaskToProject(Guid projectId, TaskRequest request);
        Task<TaskResponse> UpdateTaskAsync(Guid taskId, TaskRequest request);
    }
}
