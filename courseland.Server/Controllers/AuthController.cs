using courseland.Server.Models.DTO;
using courseland.Server.Models;
using courseland.Server.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace courseland.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private ILogger<UsersController> _logger;
        private readonly IRepository<User> _users;

        public AuthController(ILogger<UsersController> logger, IRepository<User> users)
        {
            _logger = logger;
            _users = users;
        }

        // POST: api/v1/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto login,
                                               [FromServices] IRepository<UserRole> roles)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return BadRequest("Email and password cannot be empty.");

            var users = await _users.GetAllAsync(includes: query => query.Include(u => u.Role));
            var target = users.FirstOrDefault(x => x.Email == login.Email && x.PasswordHash == login.Password, null);
            if (target is null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, target.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, target.Role.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return Ok(new
            {
                id = target.Id,
                name = target.Name,
                email = target.Email,
                role = target.Role.Name,
            });
        }

        // GET: api/v1/auth/logout
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
