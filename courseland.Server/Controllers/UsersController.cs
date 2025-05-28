using System.Security.Claims;
using courseland.Server.Filters;
using courseland.Server.Models;
using courseland.Server.Models.DTO;
using courseland.Server.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private readonly IUserRepository _users;

        public UsersController(ILogger<UsersController> logger, IUserRepository users)
        {
            _logger = logger;
            _users = users;
        }

        // GET: api/v1/users
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
        {
            var users = await _users.GetAllWithRoleAsync();

            return Ok(users.Select(UserDto.FromUser));
        }

        // GET: api/v1/users/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        [NotFoundExceptionFilter]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _users.GetByIdWithRoleAsync(id);

            return Ok(UserDto.FromUser(user));
        }

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

        // Patch: api/v1/users/change_role
        [HttpPatch("change_role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeRole(int id, string roleName, [FromServices] IRepository<UserRole> roles)
        {
            var role = (await roles.GetAllAsync(
                    filter: r => r.Name == roleName
                )).First();

            if (role == null)
            {
                return NotFound();
            }

            await _users.ChangeRoleAsync(id, role);
            await _users.SaveChangesAsync();
            return Ok();
        }
    }
}
