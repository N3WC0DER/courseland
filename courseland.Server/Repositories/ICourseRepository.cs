using courseland.Server.Models;

namespace courseland.Server.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        public Task<IEnumerable<Course>> GetAllWithCategoryAsync();
        public Task<Course> GetByIdWithCategoryAsync(int id);
        public Task ApplyDiscountAsync(int id, decimal newPrice);
        public Task DisableAsync(int id);
        public Task EnableAsync(int id);
    }
}
