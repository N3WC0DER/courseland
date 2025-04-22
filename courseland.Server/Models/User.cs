namespace courseland.Server.Models
{
    public class User
    {

        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required DateTime DateOfRegistration { get; set; }
        public required string Password { get; set; }

        //public required Role Role { get; set; }
        
    }
}
