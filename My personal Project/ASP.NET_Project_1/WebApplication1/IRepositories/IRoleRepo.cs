using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models.Roles;
namespace WebApplication1.IRepositories
{
    public interface IRoleRepo
    {

        Task<Role> GetRoleByNameAsync(string roleName); // Get role by name
        Task<List<Role>> GetAllRolesAsync();
    }
}
