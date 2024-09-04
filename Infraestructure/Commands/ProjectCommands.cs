using Aplication.Interfaces;
using CRMSystem.Data;
using CRMSystem.Models;
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
            _context.Add(project);
            await _context.SaveChangesAsync();
        }
        public Task AddTaskToProject()
        {
            throw new NotImplementedException();
        }
  
        public Task UpdateTaskToProject()
        {
            throw new NotImplementedException();
        }

        public async Task InsertInteraction(Interactions interaction)
        {
           _context.Add(interaction);
            await _context.SaveChangesAsync();
        }
    }
}
