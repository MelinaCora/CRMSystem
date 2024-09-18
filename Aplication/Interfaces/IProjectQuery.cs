
using Aplication.Pagination;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IProjectQuery
    {
        Task<Projects> GetProjectByIDAsync(Guid projectId);

        Task<Projects> GetProjectByNameAsync(string projectName);

        Task<PagedResult<Projects>> GetProjectsAsync(string projectName, 
            int? campaignTypeId, 
            int? clientId, 
            int? pageNumber, 
            int? pageSize);
        Task<Projects> GetProjectByIdAsync(Guid projectId);
    }

}

