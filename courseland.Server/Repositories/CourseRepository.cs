using courseland.Server.Exceptions;
using courseland.Server.Models;
using courseland.Server.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace courseland.Server.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationContext context) : base(context) { }

        public async Task ApplyDiscountAsync(int id, decimal newPrice)
        {
            var course = await GetByIdAsync(id);

            if (course == null)
            {
                throw new NotFoundException("Course not found");
            }

            course.Price = newPrice;
        }

        public async Task DisableAsync(int id)
        {
            var course = await GetByIdAsync(id);

            if (course == null)
            {
                throw new NotFoundException("Course not found");
            }

            course.IsActive = false;
        }

        public async Task EnableAsync(int id)
        {
            var course = await GetByIdAsync(id);

            if (course == null)
            {
                throw new NotFoundException("Course not found");
            }

            course.IsActive = true;
        }

        public async Task<IEnumerable<Course>> GetAllWithCategoryAsync()
            => await this.GetAllAsync(
                includes: query => query.Include(c => c.Category)
            );

        public async Task<Course> GetByIdWithCategoryAsync(int id)
        {
            var course = await this.GetByIdAsync(id,
                includes: query => query.Include(c => c.Category)
            );

            if (course == null)
            {
                throw new NotFoundException("Course not found");
            }

            return course;
        }
    }
}
