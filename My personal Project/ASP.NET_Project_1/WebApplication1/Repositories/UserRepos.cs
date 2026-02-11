using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.IRepositories;
using WebApplication1.Models.Users;

namespace WebApplication1.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get a user by email
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                        .Include(u => u.Role)
                        .Include(u => u.UserProfile)
                        .FirstOrDefaultAsync(u => u.UserProfile.Email == email);
        }
        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _context.UserProfiles.AnyAsync(p => p.Email == email);
        }
        public async Task AddUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            // Add user profile first if needed
            if (user.UserProfile != null)
            {
                await _context.UserProfiles.AddAsync(user.UserProfile);
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

    }
}