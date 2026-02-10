using BCrypt.Net;
using HRMS.API.DTOs;
using HRMS.DAL.Data;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly HRMSDbContext _context;

        public AdminController(HRMSDbContext context)
        {
            _context = context;
        }

        // POST: api/admin/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var admin = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (admin == null)
                return Unauthorized(new { message = "Invalid username" });

            //if (!BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash))
            //    return Unauthorized(new { message = "Invalid password" });
            if (admin.PasswordHash != dto.Password)
                return Unauthorized(new { message = "Invalid password" });

            if (!admin.IsActive)
                return Unauthorized(new { message = "Admin account is inactive" });

            if (admin.Role == null || admin.Role.RoleName != "Admin")
                return Unauthorized(new { message = "Access denied. Not an Admin." });

            return Ok(new
            {
                adminId = admin.UserId,
                userName = admin.UserName,
                fullName = admin.UserProfile.FullName,
                email = admin.UserProfile.Email,
                roleName = admin.Role.RoleName
            });
        }

        // POST: api/admin/create-user
        [HttpPost("create-user")]
        //[Authorize(Roles = "Admin")]  // ✅ Only Admin can access
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto dto)
        {
            // Validate role exists
            var roleExists = await _context.Roles.AnyAsync(r => r.RoleId == dto.RoleId);
            if (!roleExists)
                return BadRequest(new { message = "Invalid RoleId" });

            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName))
                return BadRequest(new { message = "Username already exists" });

            // Check if email already exists
            if (await _context.UserProfile.AnyAsync(p => p.Email == dto.Email))
                return BadRequest(new { message = "Email already exists" });

            // Hash password with BCrypt
            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var hashedPassword = dto.Password;
            // Create new user
            var newUser = new Users
            {
                UserName = dto.UserName,
                PasswordHash = hashedPassword,
                RoleId = dto.RoleId,
                IsActive = true,
                CreateAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Create user profile
            var newProfile = new UserProfile
            {
                UserId = newUser.UserId,
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                
            };

            _context.UserProfile.Add(newProfile);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User created successfully",
                userId = newUser.UserId,
                userName = newUser.UserName,
                fullName = newProfile.FullName,
                email = newProfile.Email
            });
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out successfully" });
        }
    }
}