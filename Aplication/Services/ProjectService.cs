﻿using Aplication.Interfaces;
using Aplication.Request;
using Aplication.Responses;
using CRMSystem.Models;
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
                ProjectName =project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                CampaignTypes= new CreateCampaignTypesResponse
                {
                   Name=project.CampaignTypes.Name,

                },
                Clients=new CreateClientResponse
                {
                    Name= project.Clients.Name,
                    Email=project.Clients.Email,
                    Phone=project.Clients.Phone,
                    Company=project.Clients.Company,
                    Address=project.Clients.Address
                }

            };
        }
    }
}
