using Aplication.Interfaces;
using CRMSystem.Data;
using CRMSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Projects>> GetProjectsAsync(string projectName = null, int? campaignTypeId = null, int? clientId = null, int pageNumber = 1, int pageSize = 10)
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

            // Implementa paginación
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Projects> GetProjectAsync(Guid projectId)
        {
           return await _context.Projects
                .Include(p=> p.Clients)
                .Include(p => p.CampaignTypes)
                .Include(p=> p.Interactions)
                .Include(p => p.TaskStatus)
                .FirstOrDefaultAsync(p => p.ProjectID ==projectId);            
        }

    }
}
