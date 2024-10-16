using Aplication.Interfaces;
using Aplication.Responses;
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

        public async Task<List<CreateUsersResponse>> GetAll()
        {
            List<Users> users = new List<Users>();
            users = await _query.GetAllUsers();
            List<CreateUsersResponse> usersResponse = users.Select(user => new CreateUsersResponse
            {
                id = user.UserID,
                name = user.Name,
                email = user.Email
            }).ToList();

            return usersResponse;
        }
    }
}
