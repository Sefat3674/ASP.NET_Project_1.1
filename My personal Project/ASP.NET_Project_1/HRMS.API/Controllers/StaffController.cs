using HRMS.DAL.Data;
using HRMS.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly HRMSDbContext _context;

        public StaffController(HRMSDbContext context)
        {
            _context = context;
        }

        // POST: api/employee/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var employee = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (employee == null)
                return Unauthorized(new { message = "Invalid username" });

            // BCrypt password verification
            if (dto.Password != employee.PasswordHash)
                return Unauthorized(new { message = "Invalid password" });

            if (!employee.IsActive)
                return Unauthorized(new { message = "Employee account is inactive" });

            if (employee.Role == null || employee.Role.RoleName != "Staff")
                return Unauthorized(new { message = "Access denied. Not an Employee." });

            return Ok(new
            {
                userId = employee.UserId,
                userName = employee.UserName,
                fullName = employee.UserProfile.FullName,
                email = employee.UserProfile.Email,
                roleName = employee.Role.RoleName
            });
        }

        // POST: api/employee/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out successfully" });
        }
    }
}