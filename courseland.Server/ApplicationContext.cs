using courseland.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace courseland.Server
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Application> Applications { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // enums to string
            modelBuilder.Entity<Application>()
                .Property(o => o.Status)
                .HasConversion<string>();


            // constraints
            var constraints = new Dictionary<string, string>
            {
                {"ValidEmail", "Email LIKE '%@%.%'"},
                {"ValidPhone", "Phone LIKE '+7[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'"},
                {"ValidDate", "RegisteredAt <= GETDATE()"}
            };

            // email constraints
            modelBuilder.Entity<Application>()
                .ToTable(t => t.HasCheckConstraint("ValidEmail", constraints["ValidEmail"]));

            modelBuilder.Entity<User>()
                .ToTable(t => t.HasCheckConstraint("ValidEmail", constraints["ValidEmail"]));

            // phone constraints
            modelBuilder.Entity<Application>()
                .ToTable(t => t.HasCheckConstraint("ValidPhone", constraints["ValidPhone"]));

            // date constraints
            modelBuilder.Entity<User>()
                .ToTable(t => t.HasCheckConstraint("ValidDate", constraints["ValidDate"]));

            // unique constraint
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
