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
using TaskStatus = CRMSystem.Models.TaskStatus;

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

            if (string.IsNullOrEmpty(request.name))
            {
                throw new RequiredParameterException("Error. Name is required");
            }

            if (request.campaignType == null)
            {
                throw new RequiredParameterException("Error. CampaignID is required");
            }

            if (request.client == null)
            {
                throw new RequiredParameterException("Error. ClientID is required");
            }

            var existingProject = await _query.GetProjectByNameAsync(request.name);
            
            if (existingProject != null)
            {
                throw new ProjectNameAlredyExistException("A project with this name already exists.");
            }

            var campaignType = await _campaignTypeQuery.GetCampaignTypeByIdAsync(request.campaignType);

            if (campaignType == null)
            {
                throw new ObjectNotFoundException("CampaignType not found.");
            }
            var client = await _clientQuery.GetClientByIdAsync(request.client);

            if (client == null)
            {
                throw new ObjectNotFoundException("Client not found.");
            }
            Projects project = new Projects
            {
                ProjectName = request.name,
                StartDate = request.start,
                EndDate = request.end,
                CampaignType = campaignType.Id,
                Clients = client,
            };

            await _command.InsertProject(project);
            var projectId = project.ProjectID;
            return new CreateProjectResponse
            {
                id= projectId,
                name = project.ProjectName,
                start= project.StartDate,
                end = project.EndDate,
                campaignType = new CreateCampaignTypesResponse
                {
                    id = project.CampaignType,
                    name = project.CampaignTypes.Name
                },
                client = new CreateClientResponse
                {
                    id = client.ClientID,
                    name = client.Name,
                    email = client.Email,
                    phone = client.Phone,
                    company = client.Company,
                    address = client.Address                    
                },
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
                Data = new CreateProjectResponse
                {
                    id = project.ProjectID,
                    name = project.ProjectName,
                    start= project.StartDate,
                    end = project.EndDate,
                    campaignType = new CreateCampaignTypesResponse
                    {
                        id = project.CampaignType,
                        name = project.CampaignTypes.Name

                    },
                    client = new CreateClientResponse
                    {
                        id = project.Clients.ClientID,
                        name = project.Clients.Name,
                        email = project.Clients.Email,
                        phone = project.Clients.Phone,
                        company = project.Clients.Company,
                        address = project.Clients.Address                       
                    }
                },
                Interactions = project.Interaction != null && project.Interaction.Any()
                       ? project.Interaction.Select(i => new InteractionResponse
                       {
                           id = i.InteractionID,
                           notes = i.Notes,
                           interactionType= new CreateInteractionTypeResponse
                           {
                               id = i.InteractionType,
                               name=i.Interactionstype.Name,

                           }
                       }).ToList()
                       : new List<InteractionResponse>(),
                Tasks = project.TaskStatus != null && project.TaskStatus.Any()
                ? project.TaskStatus.Select(t => new TaskResponse
                {
                    TaskId = t.TaskID,
                    TaskName = t.Name,
                    Users = new CreateUsersResponse
                    {
                        UserID = t.AssignedTo,
                        Name= t.AssignedUser.Name,
                        Email=t.AssignedUser.Email,                 
                    },
                    TaskStatus = new CreateTaskStatusResponse
                    {
                        Id= t.StatusId,
                        Name=t.Status.Name,
                    }
                }).ToList()
                : new List<TaskResponse>()

            };
        }

        public async Task<List<CreateProjectResponse>> GetProjectsAsync(string? Name, 
            int? campaign, 
            int? client, 
            int? offset, 
            int? limit)
        {
            // Validación de números negativos
            if (offset.HasValue && offset.Value < 0)
            {
                throw new InvalidOffsetException("El valor de offset no puede ser negativo.");
            }

            if (limit.HasValue && limit.Value <= 0)
            {
                throw new InvalidSizeException("El valor de size debe ser mayor que cero.");
            }
            if (!offset.HasValue)
            {
                offset = 0; // por defecto 0 si el campo esta vacio
            }
            if (!limit.HasValue)
            {
                limit = 10; // Si no se asigna un size, valor por defecto 10
            }
         

            var projects = await _query.GetProjectsAsync(Name, campaign, client, offset, limit);
            var responseProjects = new List<CreateProjectResponse>();

            foreach (var project in projects)
            {
                var responseProject = new CreateProjectResponse
                {
                    id = project.ProjectID,
                    name = project.ProjectName,
                    start= project.StartDate,
                    end = project.EndDate,                    
                    client = project.Clients != null ? new CreateClientResponse
                    {
                        id = project.Clients.ClientID,
                        name = project.Clients.Name,
                        email = project.Clients.Email,
                        company = project.Clients.Company,
                        phone = project.Clients.Phone,
                        address = project.Clients.Address,
                    }: null,
                    campaignType = project.CampaignTypes != null? new CreateCampaignTypesResponse
                    {
                        id = project.CampaignTypes.Id,
                        name = project.CampaignTypes.Name
                    }: null,                 
                };
                responseProjects.Add(responseProject);
            }
            return responseProjects;
        }

        public async Task<bool> AddInteractionAsync(Guid projectId,CreateInteractionRequest request)
        {
            var project = await _query.GetProjectByIdAsync(projectId);

            if (project == null)
            {
                throw new ObjectNotFoundException($"No se encontró un proyecto con el ID ");
            }

            var newinteraction = new Interactions
            {
                ProjectID = projectId,
                InteractionID = Guid.NewGuid(),
                InteractionType = request.InteractionType,
                Date = request.InteractionDate,
                Notes = request.Description
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
                Users = task.AssignedUser != null ? new CreateUsersResponse
                {
                    UserID= task.AssignedTo,
                    Name= task.AssignedUser.Name,
                    Email=task.AssignedUser.Email
                } : null,
                TaskStatus = task.Status != null ? new CreateTaskStatusResponse
                {
                    Id = task.Status.Id,
                    Name = task.Status.Name,
                } : null,
            };

            return taskResponse;
        }
    }
}
