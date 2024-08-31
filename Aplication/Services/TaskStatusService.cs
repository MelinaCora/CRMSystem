using Aplication.Interfaces;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = CRMSystem.Models.TaskStatus;

namespace Aplication.Services
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly ITaskStatusQuery _query;

        public TaskStatusService(ITaskStatusQuery query)
        {
            _query = query;
        }

        public async Task<List<TaskStatus>> GetAll()
        {
            List<TaskStatus> status = new List<TaskStatus>();
            status = await _query.GetAllTaskStatus();
            return status;
        }

    }
}
