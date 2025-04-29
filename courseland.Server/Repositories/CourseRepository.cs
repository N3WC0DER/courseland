using courseland.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace courseland.Server.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Course>> GetActiveCoursesAsync()
            => await _context.Courses.Where(c => c.IsActive).ToListAsync();

        public async Task<IEnumerable<Course>> GetByCategoryAsync(int categoryId)
            => await _context.Courses
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();
    }
}
