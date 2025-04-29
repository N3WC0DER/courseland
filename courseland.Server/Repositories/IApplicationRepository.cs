using courseland.Server.Models;

namespace courseland.Server.Repositories
{
    public interface IApplicationRepository : IRepository<Application>
    {
        public Task<IEnumerable<Application>> GetByStatusAsync(ApplicationStatus status);
        public Task<int> GetCountByCourseAsync(int courseId);
    }
}
