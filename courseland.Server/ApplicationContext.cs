using courseland.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace courseland.Server
{
    public class ApplicationContext : DbContext
    {
        

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // enums to string
        //    modelBuilder.Entity<RegistrationRequest>()
        //        .Property(o => o.Status)
        //        .HasConversion<string>();

        //    modelBuilder.Entity<ExportRequest>()
        //        .Property(o => o.Status)
        //        .HasConversion<string>();

        //    modelBuilder.Entity<Truck>()
        //        .Property(o => o.Status)
        //        .HasConversion<string>();

        //    var constraints = new Dictionary<string, string>
        //    {
        //        {"ValidEmail", "Email LIKE '%@%.%'"},
        //        {"ValidPhone", "Phone LIKE '+7[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'"},
        //        {"ValidBirthdate", "YEAR(GETDATE()) - YEAR(Birthdate) > 18 OR " +
        //                           "(YEAR(GETDATE()) - YEAR(Birthdate) = 18 AND " +
        //                           "(MONTH(GETDATE()) > MONTH(Birthdate) OR " +
        //                           "(MONTH(GETDATE()) = MONTH(Birthdate) AND " +
        //                           "DAY(GETDATE()) >= DAY(Birthdate)))) "}
        //    };

        //    // email constraints
        //    modelBuilder.Entity<RegistrationRequest>()
        //        .ToTable(t => t.HasCheckConstraint("ValidEmail", constraints["ValidEmail"]));

        //    modelBuilder.Entity<User>()
        //        .ToTable(t => t.HasCheckConstraint("ValidEmail", constraints["ValidEmail"]));

        //    // phone constraints
        //    modelBuilder.Entity<RegistrationRequest>()
        //        .ToTable(t => t.HasCheckConstraint("ValidPhone", constraints["ValidPhone"]));

        //    modelBuilder.Entity<User>()
        //        .ToTable(t => t.HasCheckConstraint("ValidPhone", constraints["ValidPhone"]));

        //    // birthdate constraint
        //    modelBuilder.Entity<Supplier>()
        //        .ToTable(t => t.HasCheckConstraint("ValidBirthdate", constraints["ValidBirthdate"]));
        //}
    }
}
