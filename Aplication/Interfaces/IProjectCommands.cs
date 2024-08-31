﻿using CRMSystem.Models;
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
        Task AddInteractionToProject();
        Task AddTaskToProject();
        Task UpdateTaskToProject();
    }
}
