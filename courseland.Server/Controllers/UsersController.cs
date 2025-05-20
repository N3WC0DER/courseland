using System.Data;
using System.Security.Claims;
using AgroLink.Server.Filters;
using courseland.Server.Models;
using courseland.Server.Models.DTO;
using courseland.Server.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace courseland.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {

        private ILogger<UsersController> _logger;
        private readonly IRepository<User> _users;

        public UsersController(ILogger<UsersController> logger, IRepository<User> users)
        {
            _logger = logger;
            _users = users;
        }

        // POST: api/v1/users/login
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
                name = target.Name,
                email = target.Email,
                role = target.Role.Name,
            });
        }

        // GET: api/v1/users/logout
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        // GET: api/v1/users
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
            => Ok(await _users.GetAllAsync(
                    includes: query => query.Include(u => u.Role)
                ));

        // GET: api/v1/users/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _users.GetByIdAsync(id,
                    includes: query => query.Include(u => u.Role)
                ));

        // GET: api/v1/users/roles
        [HttpGet("roles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetRoles([FromServices] IRepository<UserRole> roles)
            => Ok(await roles.GetAllAsync());

        // todo: add postRole();

        // POST: api/v1/users
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] UserDto userDto, [FromServices] IRepository<UserRole> roles)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = userDto.ToUser();
            var role = await roles.GetAllAsync(
                    filter: role => role.Name == user.Role.Name
                );

            if (role.IsNullOrEmpty())
                return BadRequest("Role is invalid."); // when validation attribute is done delete it

            user.Role = role.First();

            await _users.AddAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, userDto);
        }

        // PUT: api/v1/users
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Put(UserDto userDto, [FromServices] IRepository<UserRole> roles)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = userDto.ToUser();
            var role = await roles.GetAllAsync(
                    filter: role => role.Name == user.Role.Name
                );

            if (role.IsNullOrEmpty())
                return BadRequest("Role is invalid."); // when validation attribute is done delete it

            user.Role = role.First();

            await _users.UpdateAsync(user);
            return Ok(userDto);
        }

        // DELETE: api/v1/users/{id}
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _users.DeleteAsync(id);
            return NoContent();
        }
    }
}
