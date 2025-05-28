using courseland.Server.Models;

namespace courseland.Server.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IEnumerable<User>> GetAllWithRoleAsync();
        public Task<User> GetByIdWithRoleAsync(int id);
        public Task ChangeRoleAsync(int id, UserRole role);
    }
}
