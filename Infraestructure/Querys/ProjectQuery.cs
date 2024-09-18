using Aplication.Interfaces;
using Aplication.Pagination;
using CRMSystem.Data;
using CRMSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Querys
{
    public class ProjectQuery : IProjectQuery
    {
        private readonly CrmContext _context;

        public ProjectQuery(CrmContext context)
        {
            _context = context;
        }

       
        public async Task<Projects> GetProjectByIDAsync(Guid projectId)
        {
           return await _context.Projects
                .Include(p=> p.Clients)
                .Include(p => p.CampaignTypes)
                .Include(p=> p.Interaction)
                .Include(p => p.TaskStatus)
                .FirstOrDefaultAsync(p => p.ProjectID ==projectId);            
        }

        public async Task<Projects> GetProjectByNameAsync(string projectName)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(p => p.ProjectName == projectName);
        }

        public async Task<PagedResult<Projects>> GetProjectsAsync(string? projectName, int? campaignTypeId, int? clientId, int? offset, int? limit)
        {
            var query = _context.Projects.AsQueryable();

            
            if (!string.IsNullOrEmpty(projectName))
            {
                query = query.Where(p => p.ProjectName.Contains(projectName));
            }
            if (campaignTypeId.HasValue)
            {
                query = query.Where(p => p.CampaignType == campaignTypeId.Value);
            }
            if (clientId.HasValue)
            {
                query = query.Where(p => p.ClientID == clientId.Value);
            }

            int totalItems = await query.CountAsync();

            //LIMIT es en mysql
            List<Projects> projects = await query
                .Skip(offset.Value) // Salta el número de filas definidas por offset
                .Take(limit.Value)  // Toma el número de filas definidas por limit
                .ToListAsync();

            var pagedResult = new PagedResult<Projects>
            {
                Items = projects,
                TotalItems = totalItems,
                PageNumber = (offset ?? 0) / (limit ?? 10) + 1,
                PageSize = limit.Value
            };

            return pagedResult;
        }

        public async Task<Projects> GetProjectByIdAsync(Guid projectId)
        {
            return await _context.Projects
                                 .Include(p => p.CampaignTypes)
                                 .Include(p => p.Clients)
                                 .Include(p => p.TaskStatus)
                                 .Include(p => p.Interaction)
                                 .FirstOrDefaultAsync(p => p.ProjectID == projectId);
        }

    }
}
