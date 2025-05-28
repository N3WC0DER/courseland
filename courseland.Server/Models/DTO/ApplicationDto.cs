namespace courseland.Server.Models.DTO
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required int CourseId { get; set; }
        public string Status { get; set; } = "New";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }

        public static ApplicationDto FromApplication(Application application) => new ApplicationDto
        {
            Id = application.Id,
            FullName = application.FullName,
            Email = application.Email,
            Phone = application.Phone,
            CourseId = application.Course.Id,
            Status = application.Status.ToString(),
            CreatedAt = application.CreatedAt,
            Notes = application.Notes,
        };

        public Application ToApplication(Course course)
        {
            var status = (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), Status);
            return new Application
                {
                    Id = Id,
                    FullName = FullName,
                    Email = Email,
                    Phone = Phone,
                    Course = course,
                    Status = status,
                    CreatedAt = CreatedAt,
                    Notes = Notes
                };
        }
    }
}
