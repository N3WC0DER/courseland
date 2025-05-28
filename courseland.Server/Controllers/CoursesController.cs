using courseland.Server.Filters;
using courseland.Server.Models;
using courseland.Server.Models.DTO;
using courseland.Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace courseland.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CoursesController : ControllerBase
    {
        private ILogger<CoursesController> _logger;
        private readonly ICourseRepository _courses;

        public CoursesController(ILogger<CoursesController> logger, ICourseRepository courses)
        {
            _logger = logger;
            _courses = courses;
        }

        // GET: api/v1/courses
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courses = await _courses.GetAllWithCategoryAsync();

            return Ok(courses.Select(CourseDto.FromCourse));
        }

        // GET: api/v1/courses/{id}
        [HttpGet("{id}")]
        [NotFoundExceptionFilter]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _courses.GetByIdWithCategoryAsync(id);

            return Ok(CourseDto.FromCourse(course));
        }

        // POST: api/v1/courses
        [HttpPost]
        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Post([FromBody] CourseDto courseDto, [FromServices] IRepository<Category> categories)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Course course = courseDto.ToCourse();
            var category = await categories.GetAllAsync(
                    filter: category => category.Name == course.Category.Name
                );

            if (category.IsNullOrEmpty())
                return BadRequest("Category is invalid."); // when validation attribute is done delete it

            course.Category = category.First();

            await _courses.AddAsync(course);
            return CreatedAtAction(nameof(Get), new { id = course.Id }, courseDto);
        }

        // PUT: api/v1/courses
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Put([FromBody] CourseDto courseDto, [FromServices] IRepository<Category> categories)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Course course = courseDto.ToCourse();
            var category = await categories.GetAllAsync(
                    filter: category => category.Name == course.Category.Name
                );

            if (category.IsNullOrEmpty())
                return BadRequest("Category is invalid."); // when validation attribute is done delete it

            course.Category = category.First();

            await _courses.UpdateAsync(course);
            return Ok(courseDto);
        }

        // DELETE: api/v1/courses/{id}
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courses.DeleteAsync(id);
            return NoContent();
        }

        // PATCH: api/v1/courses/apply_discount
        [HttpPatch("apply_discount")]
        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> ApplyDiscount(int id, decimal newPrice)
        {
            await _courses.ApplyDiscountAsync(id, newPrice);
            await _courses.SaveChangesAsync();
            return Ok();
        }

        // PATCH: api/v1/courses/disable
        [HttpPost("disable")]
        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Disable(int id)
        {
            await _courses.DisableAsync(id);
            await _courses.SaveChangesAsync();
            return Ok();
        }

        // PATCH: api/v1/courses/enable
        [HttpPost("enable")]
        [Authorize(Roles = "manager, admin")]
        public async Task<IActionResult> Enable(int id)
        {
            await _courses.EnableAsync(id);
            await _courses.SaveChangesAsync();
            return Ok();
        }
    }
}
