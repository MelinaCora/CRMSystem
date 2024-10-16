﻿using Aplication.Interfaces;
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
    public class TaskQuery : ITaskQuery
    {
        private readonly CrmContext _context;

        public TaskQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<Tasks> GetTaskByIdAsync(Guid taskId)
        {
            return await _context.Tasks                                
                                 .Include(t => t.AssignedUser)
                                 .Include(t => t.TaskStatus)                                                  
                                 .FirstOrDefaultAsync(t => t.TaskID == taskId);

        }

        public async Task<List<Tasks>> GetAllTaskByIdAsync(Guid ProjectID)
        {
            return await _context.Tasks               
                .Include(t => t.AssignedUser)
                .Include(t => t.TaskStatus)
                .Where(t => t.ProjectID == ProjectID)
                .ToListAsync();
        }
    }
}
