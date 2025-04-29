using courseland.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace courseland.Server.Repositories
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Application>> GetByStatusAsync(ApplicationStatus status)
            => await _context.Applications
                .Where(a => a.Status == status)
                .Include(a => a.Course)
                .ToListAsync();

        public async Task<int> GetCountByCourseAsync(int courseId)
            => await _context.Applications
                .Include(a => a.Course)
                .CountAsync(a => a.Course.Id == courseId);
    }
}
