using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.IRepositories;
using WebApplication1.Models.Users;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IRoleRepo _roleRepo;

        public SignUpController(IUserRepo userRepository, IRoleRepo roleRepository)
        {
            _userRepo = userRepository;
            _roleRepo = roleRepository;
        }

        // =========================
        // Razor Views
        // =========================

        // GET: /SignUp/SignUp
        public IActionResult SignUp()
        {
            return View(); // Views/SignUp/SignUp.cshtml
        }

        // GET: /SignUp/Login
        public IActionResult Login()
        {
            return View(); // Views/SignUp/Login.cshtml
        }

        // =========================
        // JSON/AJAX Endpoints
        // =========================

        // POST: /SignUp/Register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data!" });

            if (await _userRepo.IsEmailExistAsync(model.Email))
                return Json(new { success = false, message = "Email already registered." });

            var role = await _roleRepo.GetRoleByNameAsync("Customer");

            var user = new User
            {
                UserName = model.Email,
                RoleId = role.RoleId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                UserProfile = new WebApplication1.Models.UserProfile.UserProfile
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Phone = model.Phone
                }
            };

            await _userRepo.AddUserAsync(user);

            return Json(new { success = true, message = "Registration successful!" });
        }

        // POST: /SignUp/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data!" });

            var user = await _userRepo.GetUserByEmailAsync(model.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return Json(new { success = false, message = "Invalid email or password." });

            // Save session info
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("RoleName", user.Role.RoleName);

            return Json(new
            {
                success = true,
                message = "Login successful!",
                role = user.Role.RoleName
            });
        }

        // POST: /SignUp/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Json(new { success = true, message = "Logged out successfully!" });
        }
    }
}