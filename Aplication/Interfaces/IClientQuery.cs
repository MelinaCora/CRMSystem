using Aplication.Responses;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IClientQuery
    {
        Task <List<Clients>> GetListClientsAsync();
        Task<Clients> GetClientByIdAsync(int clientId);
    }
}
