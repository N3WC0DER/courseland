namespace courseland.Server.Models
{
    public class Course
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required int DurationInHours { get; set; }
        public required Category Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required bool IsActive { get; set; }
        public string ImageUrl { get; set; } = ""; // todo: replace to default image

    }
}
