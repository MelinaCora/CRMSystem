using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Request;
using Aplication.Responses;
using CRMSystem.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Aplication.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectCommands _command;
        private readonly IProjectQuery _query;
        private readonly ITaskQuery _taskQuery;
        private readonly ITaskCommand _taskCommand;
        private readonly ICampaignTypeQuery _campaignTypeQuery;
        private readonly IClientQuery _clientQuery;

        public ProjectService(IProjectCommands command, IProjectQuery query, ITaskQuery taskQuery, ITaskCommand taskCommand, ICampaignTypeQuery campaignTypeQuery, IClientQuery clientQuery)
        {
            _command = command;
            _query = query;
            _taskQuery = taskQuery;
            _taskCommand = taskCommand;
            _campaignTypeQuery = campaignTypeQuery;
            _clientQuery = clientQuery;
        }

        public async Task<CreateProjectResponse> CreateProject(ProjectRequest request)
        {
            var existingProject = await _query.GetProjectByNameAsync(request.ProjectReqName);
            
            if (existingProject != null)
            {
                throw new ProjectNameAlredyExistException("A project with this name already exists.");
            }

            var campaignType = await _campaignTypeQuery.GetCampaignTypeByIdAsync(request.reqCampaignID);

            if (campaignType == null)
            {
                throw new ObjectNotFoundException("CampaignType not found.");
            }
            var client = await _clientQuery.GetClientByIdAsync(request.ClientID);

            if (client == null)
            {
                throw new ObjectNotFoundException("Client not found.");
            }

            Projects project = new Projects
            {
                ProjectName = request.ProjectReqName,
                StartDate = request.reqStartDate,
                EndDate = request.reqEndDate,
                CampaignType = campaignType.Id,
                Clients = client
            };

            await _command.InsertProject(project);
            return new CreateProjectResponse
            {
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                CampaignTypes = new CreateCampaignTypesResponse
                {
                    Id = project.CampaignType,

                },
                Clients = new CreateClientResponse
                {
                    ClientID = client.ClientID,
                    Name = client.Name,
                    Email = client.Email,
                    Phone = client.Phone,
                    Company = client.Company,
                    Address = client.Address,
                    CreateDate = DateTime.Now,
                }

            };
        }

        public async Task<ProjectDetailsResponse> GetProjectByIdAsync(Guid projectId)
        {
            var project = await _query.GetProjectByIDAsync(projectId);

            if (project == null)
            {
               throw new ObjectNotFoundException($"No se encontró un proyecto con el ID {projectId}");
            }

           
            return new ProjectDetailsResponse
            {
                ProjectId = project.ProjectID,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                CampaignType = project.CampaignType,
                ClientName = project.Clients?.Name,
                Tasks = project.TaskStatus.Select(t => new TaskResponse
                {
                    TaskId = t.TaskID,
                    TaskName = t.Name,
                    AssignedTo = t.AssignedTo,
                    Status = t.StatusId,
                }).ToList(),
                Interactions = project.Interaction.Select(i => new InteractionResponse
                {
                    InteractionId = i.InteractionID,
                    InteractionType = i.Interactionstype?.Name,
                    Notes = i.Notes
                }).ToList()
            };
        }
    

        public async Task<PagedResult<Projects>> GetProjectsAsync(string? projectName= null, 
            int? campaignTypeId = null, 
            int? clientId = null, 
            int pageNumber = 1, 
            int pageSize = 10)
        {
            return await _query.GetProjectsAsync(projectName, campaignTypeId, clientId, pageNumber, pageSize);
        }

        public async Task<bool> AddInteractionAsync(CreateInteractionRequest request)
        {
            var project = await _query.GetProjectByIdAsync(request.ProjectId);

            if (project == null)
            {
                throw new ObjectNotFoundException($"No se encontró un proyecto con el ID ");
            }

            var newinteraction = new Interactions
            {
                InteractionID = Guid.NewGuid(),
                ProjectID=request.ProjectId,
                InteractionType= request.InteractionType,
                Date= request.InteractionDate,
                Notes=request.Description
            };

            await _command.InsertInteraction(newinteraction);
            return true;
        }

        public async Task<bool> AddTaskToProject(Guid projectId, TaskRequest request)
        {
            var project = await _query.GetProjectByIDAsync(projectId);

            if (project == null)
            {
                throw new ObjectNotFoundException($"No se encontró un proyecto con el ID {projectId}");
            }

            var task = new Tasks
            {
                TaskID = Guid.NewGuid(),
                Name = request.TaskName,
                DueDate = request.DueDate,
                ProjectID = projectId,
                AssignedTo = request.AssignedTo,
                StatusId = request.StatusId
            };

            await _command.AddTaskToProject(task);
            return true;
        }

        public async Task<TaskResponse> UpdateTaskAsync(Guid taskId, TaskRequest request)
        {
            
            var task = await _taskQuery.GetTaskByIdAsync(taskId);

            if (task == null)
            {
                throw new ObjectNotFoundException("Task not found.");
            }

            
            task.Name = request.TaskName;
            task.DueDate = request.DueDate;
            task.AssignedTo = request.AssignedTo;
            task.StatusId = request.StatusId;

            
            await _taskCommand.UpdateTaskAsync(task); 

            
            var taskResponse = new TaskResponse
            {
                TaskId = task.TaskID,
                TaskName = task.Name,
                AssignedTo = task.AssignedTo, 
                Status = task.StatusId          
            };

            return taskResponse;
        }
    }
}
