using courseland.Server.Exceptions;
using courseland.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace courseland.Server.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) { }

        public async Task ChangeRoleAsync(int id, UserRole role)
        {
            var user = await GetByIdWithRoleAsync(id);
            user.Role = role;
        }

        public async Task<IEnumerable<User>> GetAllWithRoleAsync()
            => await this.GetAllAsync(
                includes: query => query.Include(u => u.Role)
            );

        public async Task<User> GetByIdWithRoleAsync(int id)
        {
            var user = await this.GetByIdAsync(id,
                includes: query => query.Include(u => u.Role)
            );

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            return user;
        }
    }
}
