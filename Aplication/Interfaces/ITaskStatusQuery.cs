using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = CRMSystem.Models.TaskStatus;

namespace Aplication.Interfaces
{
    public interface ITaskStatusQuery
    {
        Task<List<TaskStatus>> GetAllTaskStatus();
    }
}
