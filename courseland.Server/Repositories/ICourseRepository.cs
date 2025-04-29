using courseland.Server.Models;

namespace courseland.Server.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        public Task<IEnumerable<Course>> GetActiveCoursesAsync();
        public Task<IEnumerable<Course>> GetByCategoryAsync(int categoryId);
    }
}
