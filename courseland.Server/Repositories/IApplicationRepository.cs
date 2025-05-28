using courseland.Server.Models;

namespace courseland.Server.Repositories
{
    public interface IApplicationRepository : IRepository<Application>
    {
        public Task SetNoteAsync(int id, string? note);
        public Task<IEnumerable<Application>> GetAllWithCourseAsync();
        public Task<Application> GetByIdWithCourseAsync(int id);
    }
}
