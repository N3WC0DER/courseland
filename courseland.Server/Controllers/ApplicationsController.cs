using System.Data;
using courseland.Server.Filters;
using courseland.Server.Models;
using courseland.Server.Models.DTO;
using courseland.Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace courseland.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApplicationsController : Controller
    {

        private ILogger<UsersController> _logger;
        private readonly IApplicationRepository _applications;

        public ApplicationsController(ILogger<UsersController> logger, IApplicationRepository applications)
        {
            _logger = logger;
            _applications = applications;
        }

        // POST: api/v1/applications/note
        [HttpPost("note")]
        [Authorize(Roles = "manager, admin")]
        [NotFoundExceptionFilter]
        public async Task<IActionResult> PostNote(int id, string note)
        {
            await _applications.SetNoteAsync(id, note);
            await _applications.SaveChangesAsync();
            
            return Ok();
        }

        // DELETE: api/v1/applications/note
        [HttpDelete("note")]
        [Authorize(Roles = "manager, admin")]
        [NotFoundExceptionFilter]
        public async Task<IActionResult> DeleteNote(int id)
        {
            await _applications.SetNoteAsync(id, null);
            await _applications.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/v1/applications
        [HttpGet]
        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Get()
        {
            var applications = await _applications.GetAllWithCourseAsync();
            return Ok(applications.Select(a => ApplicationDto.FromApplication(a)));
        }

        // GET: api/v1/applications/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "manager, admin")]
        [NotFoundExceptionFilter]
        public async Task<IActionResult> Get(int id)
        {
            var application = await _applications.GetByIdWithCourseAsync(id);
            return Ok(ApplicationDto.FromApplication(application));
        }

        // POST: api/v1/applications
        [HttpPost]
        [NotFoundExceptionFilter]
        public async Task<IActionResult> Post([FromBody] ApplicationDto applicationDto,
                                              [FromServices] ICourseRepository courses,
                                              [FromServices] IRepository<Category> categories)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = await courses.GetByIdWithCategoryAsync(applicationDto.CourseId);

            Application application = applicationDto.ToApplication(course);

            await _applications.AddAsync(application);
            return CreatedAtAction(nameof(Get), new { id = application.Id }, applicationDto);
        }

        // PUT: api/v1/applications
        [HttpPut]
        [Authorize(Roles = "manager, admin")]
        [NotFoundExceptionFilter]
        public async Task<IActionResult> Put(ApplicationDto applicationDto,
                                              [FromServices] ICourseRepository courses,
                                              [FromServices] IRepository<Category> categories)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = await courses.GetByIdWithCategoryAsync(applicationDto.CourseId);

            Application application = applicationDto.ToApplication(course);

            await _applications.UpdateAsync(application);
            return Ok(applicationDto);
        }

        // DELETE: api/v1/applications/{id}
        [HttpDelete]
        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _applications.DeleteAsync(id);
            return NoContent();
        }
    }
}
