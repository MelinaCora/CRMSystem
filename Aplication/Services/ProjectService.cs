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
        public async Task<ProjectDetailsResponse> CreateProject(ProjectRequest request)
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

            if (request.start == default(DateTime))
            {
                throw new RequiredParameterException("Error. Start date is required");
            }

            if (request.start < DateTime.Now)
            {
                throw new RequiredParameterException("Error. Start date cannot be in the past");
            }

            if (request.campaignType < 1 || request.campaignType > 4)
            {
                throw new StaticParameterException("Error. CampaignType must be between 1 and 4.");
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
                CreateDate=DateTime.Now,
            };

            await _command.InsertProject(project);
            var projectId = project.ProjectID;
            return new ProjectDetailsResponse
            {
                Data = new CreateProjectResponse
                {
                    id = project.ProjectID,
                    name = project.ProjectName,
                    start = project.StartDate,
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
                           interactionType = new CreateInteractionTypeResponse
                           {
                               id = i.InteractionType,
                               name = i.Interactionstype.Name,

                           }
                       }).ToList()
                       : new List<InteractionResponse>(),
                Tasks = project.TaskStatus != null && project.TaskStatus.Any()
                ? project.TaskStatus.Select(t => new TaskResponse
                {
                    id = t.TaskID,
                    name = t.Name,
                    userAssigned = new CreateUsersResponse
                    {
                        id = t.AssignedTo,
                        name = t.AssignedUser.Name,
                        email = t.AssignedUser.Email,
                    },
                    status = new CreateTaskStatusResponse
                    {
                        id = t.Status,
                        name = t.TaskStatus.Name,
                    }
                }).ToList()
                : new List<TaskResponse>()

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
                           projectId= i.ProjectID,
                           date=i.Date,
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
                    id = t.TaskID,
                    name = t.Name,
                    projectId=t.ProjectID,
                    dueDate = t.DueDate,
                    userAssigned = new CreateUsersResponse
                    {
                        id = t.AssignedTo,
                        name= t.AssignedUser.Name,                       
                        email =t.AssignedUser.Email,                 
                    },
                    status = new CreateTaskStatusResponse
                    {
                        id= t.Status,
                        name=t.TaskStatus.Name,
                    }
                }).ToList()
                : new List<TaskResponse>()

            };
        }

        public async Task<List<CreateProjectResponse>> GetProjectsAsync(string? name, 
            int? campaign, 
            int? client, 
            int? offset, 
            int? limit)
        {
            
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
                offset = 0;
            }
            if (!limit.HasValue)
            {
                limit = 10; 
            }

            if (campaign < 1 || campaign > 4)
            {
                throw new StaticParameterException("Error. CampaignType must be between 1 and 4.");
            }

            var projects = await _query.GetProjectsAsync(name, campaign, client, offset, limit);
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

        public async Task<InteractionResponse> AddInteractionAsync(Guid projectId,CreateInteractionRequest request)
        {
            var project = await _query.GetProjectByIDAsync(projectId);

            if (project == null)
            {
                throw new ObjectNotFoundException($"No se encontró un proyecto con el ID ");
            }

            if (string.IsNullOrEmpty(request.notes))
            {
                throw new RequiredParameterException("Error. Notes is required");
            }

            if (request.InteractionType == null)
            {
                throw new RequiredParameterException("Error. Interaction Type is Required");
            }

            if (request.InteractionType < 1 || request.InteractionType > 4)
            {
                throw new StaticParameterException("Error. Interaction Type must be between 1 and 4");
            }

            if (request.date == default(DateTime))
            {
                throw new RequiredParameterException("Error.The date is required");
            }

            if (request.date < DateTime.Now)
            {
                throw new RequiredParameterException("Error. The date cannot be in the past");
            }
            if (request.date < project.StartDate)
            {
                throw new RequiredParameterException("Error. The date cannot be earlier than the project's start date.");
            }
            var newInteraction = new Interactions
            {
                ProjectID = projectId,
                InteractionID = Guid.NewGuid(),
                InteractionType = request.InteractionType,
                Date = request.date,
                Notes = request.notes
            };

            await _command.InsertInteraction(newInteraction);
            project.UpdateDate = DateTime.Now;

            await _command.UpdateProject(project);

            var interactionResponse = new InteractionResponse
            {
                id = newInteraction.InteractionID,
                notes = newInteraction.Notes,
                date = newInteraction.Date,
                projectId = newInteraction.ProjectID,
                interactionType = newInteraction.Interactionstype != null
                ? new CreateInteractionTypeResponse
                {
                    id = newInteraction.Interactionstype.Id,
                    name = newInteraction.Interactionstype.Name
                } : null               
            };
            return interactionResponse;
        }

        public async Task<TaskResponse> AddTaskToProject(Guid projectId, TaskRequest request)
        {
            var project = await _query.GetProjectByIDAsync(projectId);

            if (project == null)
            {
                throw new ObjectNotFoundException($"No se encontró un proyecto con el ID {projectId}");
            }

            if(string.IsNullOrEmpty(request.name))
            {
                throw new RequiredParameterException("Error. The task's Name is required");
            }

            if (request.user == null)
            {
                throw new RequiredParameterException("Error. The User is required");
            }

            if (request.user < 1 || request.user > 5)
            {
                throw new StaticParameterException("Error. The User id must be between 1 and 5");
            }

            if (request.status == null)
            {
                throw new RequiredParameterException("Error. The Status is required");
            }

            if (request.status < 1 || request.status > 5)
            {
                throw new StaticParameterException("Error. The status id must be between 1 and 5");
            }

            if (request.dueDate == default(DateTime))
            {
                throw new RequiredParameterException("Error.The date is required");
            }

            if (request.dueDate < DateTime.Now)
            {
                throw new RequiredParameterException("Error. The date cannot be in the past");
            }

            if (request.dueDate < project.StartDate)
            {
                throw new RequiredParameterException("Error. The date cannot be earlier than the project's start date.");
            }

            var task = new Tasks
            {
                TaskID = Guid.NewGuid(),
                Name = request.name,
                DueDate = request.dueDate,
                ProjectID = projectId,
                AssignedTo = request.user,
                Status = request.status,
                CreateDate=DateTime.Now 
            };

            await _command.AddTaskToProject(task);

            project.UpdateDate = DateTime.Now;

            await _command.UpdateProject(project);

            var insertedTask = await _taskQuery.GetTaskByIdAsync(task.TaskID);

            return new TaskResponse
            {
                id = insertedTask.TaskID,
                name = insertedTask.Name,
                dueDate = insertedTask.DueDate,
                projectId = insertedTask.ProjectID,
                status = insertedTask.TaskStatus != null ? new CreateTaskStatusResponse
                {
                    id = insertedTask.Status,
                    name= insertedTask.TaskStatus.Name
                }:null,
                userAssigned = insertedTask.AssignedUser != null ? new CreateUsersResponse
                {
                    id= insertedTask.AssignedTo,
                    name= insertedTask.AssignedUser.Name,
                    email= insertedTask.AssignedUser.Email,
                }:null,
            };
        }

        public async Task<TaskResponse> UpdateTaskAsync(Guid taskId, TaskRequest request)
        {
            
            var task = await _taskQuery.GetTaskByIdAsync(taskId);

            if (task == null)
            {
                throw new ObjectNotFoundException("Task not found.");
            }

            if (string.IsNullOrEmpty(request.name))
            {
                throw new RequiredParameterException("Error. The task's name is required");
            }

            if (request.user == null)
            {
                throw new RequiredParameterException("Error. The user id is required");
            }

            if (request.user < 1 || request.user > 5)
            {
                throw new StaticParameterException("Error. the user id must be between 1 and 5");
            }

            if (request.status == null)
            {
                throw new RequiredParameterException("Error. The status id is required");
            }

            if (request.status < 1 || request.status > 5)
            {
                throw new StaticParameterException("Error. the status id must be between 1 and 5");
            }
          
            task.Name = request.name;
            task.DueDate = request.dueDate;
            task.AssignedTo = request.user;
            task.Status = request.status;
            task.UpdateDate = DateTime.Now;
            
            await _taskCommand.UpdateTaskAsync(task); 
            
            var taskResponse = new TaskResponse
            {
                id = task.TaskID,
                name = task.Name,
                dueDate=task.DueDate,
                projectId=task.ProjectID,
                userAssigned = task.AssignedUser != null ? new CreateUsersResponse
                {
                    id= task.AssignedUser.UserID,
                    name= task.AssignedUser.Name,
                    email=task.AssignedUser.Email
                } : null,
                status = task.TaskStatus != null ? new CreateTaskStatusResponse
                {
                    id = task.TaskStatus.Id,
                    name = task.TaskStatus.Name,
                } : null,
            };
            return taskResponse;
        }
    }
}
