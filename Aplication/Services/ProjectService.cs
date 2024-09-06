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



        public ProjectService(IProjectCommands command, IProjectQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<CreateProjectResponse> CreateProject(ProjectRequest request)
        {
            
            Projects project = new Projects
            {
                ProjectName = request.ProjectReqName,
                StartDate = request.reqStartDate,
                EndDate = request.reqEndDate,
                CampaignTypes = new CampaignTypes
                {
                    Name = request.reqCampaignName,

                },
                Clients = new Clients
                {
                    Name = request.reqClientName,
                    Email = request.reqClientEmail,
                    Phone = request.reqClientPhone,
                    Company = request.reqClientCompany,
                    Address = request.reqClientAddress
                }
            };

            await _command.InsertProject(project);
            return new CreateProjectResponse
            {
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                CampaignTypes = new CreateCampaignTypesResponse
                {
                    Name = project.CampaignTypes.Name,

                },
                Clients = new CreateClientResponse
                {
                    Name = project.Clients.Name,
                    Email = project.Clients.Email,
                    Phone = project.Clients.Phone,
                    Company = project.Clients.Company,
                    Address = project.Clients.Address
                }

            };
        }

        public async Task<ProjectDetailsResponse> GetProjectByIdAsync(Guid projectId)
        {
            var project = await _query.GetProjectByIDAsync(projectId);
            if (project == null)
            {
               throw new KeyNotFoundException($"No se encontró un proyecto con el ID {projectId}");
            }

            // Mapea el proyecto a un objeto de respuesta detallada
            return new ProjectDetailsResponse
            {
                ProjectId = project.ProjectID,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                CampaignType = project.CampaignTypes?.Name,
                ClientName = project.Clients?.Name,
                Tasks = project.TaskStatus.Select(t => new TaskResponse
                {
                    TaskId = t.TaskID,
                    TaskName = t.Name,
                    AssignedTo = t.AssignedUser.Name,
                    Status = t.Status?.name,
                }).ToList(),
                Interactions = project.Interaction.Select(i => new InteractionResponse
                {
                    InteractionId = i.InteractionID,
                    InteractionType = i.Interactionstype?.Name,
                    Notes = i.Notes
                }).ToList()
            };
        }
    

        public async Task<PagedResult<Projects>> GetProjectsAsync(string projectName = null, 
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
                throw new InvalidOperationException("Project not found.");
            }

            var newinteraction = new Interactions
            {
                ProjectID=request.ProjectId,
                InteractionType= request.InteractionType,
                Date= request.InteractionDate,
                Notes=request.Description
            };

            await _command.InsertInteraction(newinteraction);
            return true;
        }

    }
}
