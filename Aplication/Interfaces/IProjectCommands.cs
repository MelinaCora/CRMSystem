using CRMSystem.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IProjectCommands
    {
        Task InsertProject(Projects project);
        Task AddTaskToProject(Tasks task);
        Task UpdateTaskToProject(Tasks updatedTask);
        Task InsertInteraction(Interactions interaction);
    }
}
