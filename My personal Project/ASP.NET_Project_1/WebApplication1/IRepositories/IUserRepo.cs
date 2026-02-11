using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.IRepositories;
using WebApplication1.Models.Users;

namespace WebApplication1.IRepositories
{
    public interface IUserRepo
    {
        Task<User> GetUserByEmailAsync(string email); // Get user by email
        Task AddUserAsync(User user);
        Task<bool> IsEmailExistAsync(string email);
    }
}
