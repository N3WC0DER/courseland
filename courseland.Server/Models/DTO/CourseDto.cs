using Microsoft.AspNetCore.Identity;
using System.Data;

namespace courseland.Server.Models.DTO
{
    public class CourseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required int DurationInHours { get; set; }
        public required string Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required bool IsActive { get; set; }
        public string ImageUrl { get; set; } = ""; // todo: replace to default image

        public static CourseDto FromCourse(Course course) => new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Price = course.Price,
            DurationInHours = course.DurationInHours,
            Category = course.Category.Name,
            CreatedAt = course.CreatedAt,
            IsActive = course.IsActive,
            ImageUrl = course.ImageUrl
        };

        public Course ToCourse() => new Course
        {
            Id = Id,
            Title = Title,
            Description = Description,
            Price = Price,
            DurationInHours = DurationInHours,
            Category = new Category() { Name = Category },
            CreatedAt = CreatedAt,
            IsActive = IsActive,
            ImageUrl = ImageUrl
        };
    }
}
