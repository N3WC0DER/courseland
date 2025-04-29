namespace courseland.Server.Models
{
    public class Category
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
