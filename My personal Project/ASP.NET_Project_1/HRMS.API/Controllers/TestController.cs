using HRMS.DAL.Data;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly HRMSDbContext _context;

        public TestController(HRMSDbContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.Role)          // include Role
                    .Include(u => u.UserProfile)   // include Profile
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _context.Roles.ToListAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("profiles")]
        public async Task<IActionResult> GetProfiles()
        {
            try
            {
                var profiles = await _context.UserProfile
                    .Include(p => p.User)
                    .ThenInclude(u => u.Role)
                    .ToListAsync();

                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}