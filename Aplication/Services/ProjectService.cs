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

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new RequiredParameterException("Error. Name is required");
            }

            if (request.CampaignID == null)
            {
                throw new RequiredParameterException("Error. CampaignID is required");
            }

            if (request.ClientID == null)
            {
                throw new RequiredParameterException("Error. ClientID is required");
            }

            var existingProject = await _query.GetProjectByNameAsync(request.Name);
            
            if (existingProject != null)
            {
                throw new ProjectNameAlredyExistException("A project with this name already exists.");
            }

            var campaignType = await _campaignTypeQuery.GetCampaignTypeByIdAsync(request.CampaignID);

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
                ProjectName = request.Name,
                StartDate = request.Start,
                EndDate = request.End,
                CampaignType = campaignType.Id,
                Clients = client,
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
                    Name = project.CampaignTypes.Name
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
                ProjectID = project.ProjectID,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                CampaignTypes = new CreateCampaignTypesResponse
                {
                    Id = project.CampaignType,
                    Name = project.CampaignTypes.Name,
                },
                Clients = new CreateClientResponse
                {
                    ClientID = project.Clients.ClientID,
                    Name = project.Clients.Name,
                    Email = project.Clients.Email,
                    Phone = project.Clients.Phone,
                    Company = project.Clients.Company,
                    Address = project.Clients.Address,
                    CreateDate = project.Clients.CreateDate,
                },
                Tasks = project.TaskStatus != null
                ? project.TaskStatus.Select(t => new TaskResponse
                {
                    TaskId = t.TaskID,
                    TaskName = t.Name,
                    TaskStatus = new CreateTaskStatusResponse
                    {
                        Id = t.StatusId,
                    },
                    Users = new CreateUsersResponse
                    {
                        UserID = t.AssignedTo,
                    }
                }).ToList()
                : new List<TaskResponse>(), //lista vacía si no hay tareas
                Interactions = project.Interaction != null
                ? project.Interaction.Select(i => new InteractionResponse
                {
                    InteractionId = i.InteractionID,
                    InteractionType = new CreateInteractionTypeResponse
                    {
                        Id = i.InteractionType,
                    },
                    Notes = i.Notes
                }).ToList()
                : new List<InteractionResponse>() //lista vacía si no hay interacciones
            };
        }  

        public async Task<PagedResult<Projects>> GetProjectsAsync(string? Name, 
            int? campaign, 
            int? client, 
            int? offset, 
            int? size)
        {
            // Validación de números negativos
            if (offset.HasValue && offset.Value < 0)
            {
                throw new InvalidOffsetException("El valor de offset no puede ser negativo.");
            }

            if (size.HasValue && size.Value <= 0)
            {
                throw new InvalidSizeException("El valor de size debe ser mayor que cero.");
            }
            // Construir la consulta según los parámetros ingresados
            string filterMessage = "Filtrando por: ";
            if (!string.IsNullOrEmpty(Name))
            {
                filterMessage += $"Nombre = {Name}, ";
            }
            if (campaign.HasValue)
            {
                filterMessage += $"Campaña = {campaign.Value}, ";
            }
            if (client.HasValue)
            {
                filterMessage += $"Cliente = {client.Value}, ";
            }
            filterMessage += $"Offset = {offset.Value}, Size = {size.Value}";

            return await _query.GetProjectsAsync(Name, campaign, client, offset.Value, size.Value);
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
                Users = new CreateUsersResponse
                {
                    UserID= task.AssignedTo,
                    Name= task.AssignedUser.Name,
                },
                TaskStatus = new CreateTaskStatusResponse
                {
                    Id = task.Status.Id,
                    Name = task.Status.Name,
                },
            };

            return taskResponse;
        }
    }
}
