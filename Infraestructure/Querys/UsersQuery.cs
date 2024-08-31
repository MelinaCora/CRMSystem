using Aplication.Interfaces;
using CRMSystem.Data;
using CRMSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Querys
{
    public class UsersQuery : IUsersQuery
    {
        private readonly CrmContext _context;

        public UsersQuery(CrmContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
