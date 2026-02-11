using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.IRepositories;
using WebApplication1.Models.Roles;
namespace WebApplication1.Repositories
{
    public class RoleRepo : IRoleRepo
    {
        private readonly ApplicationDbContext _context;

        public RoleRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get role by name
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        // Get all roles
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
