namespace courseland.Server.Models.DTO
{
    public class UserDto
    {
        // maybe add [remote] attr ? https://metanit.com/sharp/aspnetmvc/9.3.php
        // or validation attribute with application context <- yes
        // for roles.
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public static UserDto FromUser(User user) => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Role = user.Role.Name,
            RegisteredAt = user.RegisteredAt,
        };

        public User ToUser() => new User
        {
            Id = Id,
            Name = Name,
            Email = Email,
            PasswordHash = PasswordHash,
            Role = new UserRole { Name = Role },
            RegisteredAt = RegisteredAt,
        };
    }
}
