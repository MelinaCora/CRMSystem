using Aplication.Interfaces;
using CRMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersQuery _query;

        public UserService(IUsersQuery query)
        {
            _query = query;
        }

        public async Task<List<Users>> GetAll()
        {
            List<Users> users = new List<Users>();
            users = await _query.GetAllUsers();
            return users;
        }
    }
}
