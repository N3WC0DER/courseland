namespace courseland.Server.Models
{
    public class Application
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required Course Course { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.New;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
    }
}
