namespace courseland.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required UserRole Role { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
