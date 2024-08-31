using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRMSystem.Models;
using TaskStatus = CRMSystem.Models.TaskStatus;

namespace Aplication.Interfaces
{
    public interface ITaskStatusService
    {
        Task<List<TaskStatus>> GetAll();
    }
}
