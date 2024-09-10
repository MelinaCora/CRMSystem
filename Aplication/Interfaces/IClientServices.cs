using Aplication.Request;
using Aplication.Responses;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IClientServices
    {
       Task <Clients> CreateClient(ClientRequest request);

       Task <List<Clients>> GetAll();


    }
}
