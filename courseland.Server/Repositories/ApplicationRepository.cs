using courseland.Server.Exceptions;
using courseland.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace courseland.Server.Repositories
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Application>> GetAllWithCourseAsync()
            => await this.GetAllAsync(
                includes: query => query.Include(a => a.Course).Include(a => a.Course.Category)
            );

        public async Task<Application> GetByIdWithCourseAsync(int id)
        {
            var applicaiton = await this.GetByIdAsync(id,
                includes: query => query.Include(a => a.Course).Include(a => a.Course.Category)
            );

            if (applicaiton == null)
            {
                throw new NotFoundException("Application not found");
            }

            return applicaiton;
        }

        public async Task SetNoteAsync(int id, string? note)
        {
            var application = await this.GetByIdAsync(id);
            if (application == null)
            {
                throw new NotFoundException("Application not found");
            }
            application.Notes = note;
        }
    }
}
