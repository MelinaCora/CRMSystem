using Aplication.Interfaces;
using CRMSystem.Data;
using CRMSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = CRMSystem.Models.TaskStatus;

namespace Infraestructure.Querys
{
    public class TaskStatusQuery : ITaskStatusQuery
    {
        private readonly CrmContext _context;

        public TaskStatusQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<TaskStatus>> GetAllTaskStatus()
        {
            return await _context.TaskStatus.ToListAsync();
        }
    }
}
