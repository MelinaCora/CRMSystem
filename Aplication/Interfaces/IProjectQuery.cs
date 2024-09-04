
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

        Task<PagedResult<Projects>> GetProjectsAsync(string projectName = null, 
            int? campaignTypeId = null, 
            int? clientId = null, 
            int pageNumber = 1, 
            int pageSize = 10);
        Task<Projects> GetProjectByIdAsync(Guid projectId);
    }

}

