using Aplication.Interfaces;
using CRMSystem.Data;
using CRMSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infraestructure.Commands
{
    public class ProjectCommands : IProjectCommands
    {

        private readonly CrmContext _context;

        public ProjectCommands(CrmContext context)
        {
            _context = context;
        }

        public async Task InsertProject(Projects project)
        {
            var existingProject = await _context.Projects
                .FirstOrDefaultAsync(p => p.ProjectName == project.ProjectName); 
          
            
            _context.Add(project);
            await _context.SaveChangesAsync();            
        }
        public async Task AddTaskToProject(Tasks task)
        {
            _context.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskToProject(Tasks updatedTask)
        {
            
            var existingTask = await _context.Tasks.FindAsync(updatedTask.TaskID);
                 
            existingTask.Name = updatedTask.Name;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.AssignedTo = updatedTask.AssignedTo;
            existingTask.Status = updatedTask.Status;

            
            _context.Tasks.Update(existingTask);
            await _context.SaveChangesAsync();
        }

        public async Task InsertInteraction(Interactions interaction)
        {
           _context.Add(interaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProject(Projects project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
