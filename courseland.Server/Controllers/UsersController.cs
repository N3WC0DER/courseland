using System.Data;
using AgroLink.Server.Filters;
using courseland.Server.Models;
using courseland.Server.Models.DTO;
using courseland.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        // GET: api/v1/users
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _users.GetAllAsync(
                    includes: query => query.Include(u => u.Role)
                ));

        // GET: api/v1/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _users.GetByIdAsync(id,
                    includes: query => query.Include(u => u.Role)
                ));

        // GET: api/v1/users/roles
        [HttpGet("roles")]
        [TypeFilter<BadSqlExceptionFilter>]
        public async Task<IActionResult> GetRoles([FromServices] IRepository<UserRole> roles)
            => Ok(await roles.GetAllAsync());

        // POST: api/v1/users
        [HttpPost]
        [TypeFilter<BadSqlExceptionFilter>]
        public async Task<IActionResult> Post([FromBody] UserDto userDto, [FromServices] IRepository<UserRole> roles)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = userDto.ToUser();
            var role = await roles.GetAllAsync(
                    filter: role => role.Name == user.Role.Name
                );

            if (role.IsNullOrEmpty())
                return BadRequest("Role is invalid.");

            user.Role = role.First();

            await _users.AddAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, userDto);
        }

        // PUT: api/v1/users
        [HttpPut]
        [TypeFilter<BadSqlExceptionFilter>]
        public async Task<IActionResult> Put(UserDto userDto, [FromServices] IRepository<UserRole> roles)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = userDto.ToUser();
            var role = await roles.GetAllAsync(
                    filter: role => role.Name == user.Role.Name
                );

            if (role.IsNullOrEmpty())
                return BadRequest("Role is invalid.");

            user.Role = role.First();

            await _users.UpdateAsync(user);
            return Ok(userDto);
        }

        // DELETE: api/v1/users/{id}
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _users.DeleteAsync(id);
            return NoContent();
        }
    }
}
