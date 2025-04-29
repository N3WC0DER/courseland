using courseland.Server.Models;

namespace courseland.Server
{
    public static class MockDbInitializer
    {

        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return;
            }

            // add categories
            var categories = new Category[]
            {
            new Category { Name = "Программирование" },
            new Category { Name = "Дизайн" },
            new Category { Name = "Маркетинг" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            

            // add courses
            var courses = new Course[]
            {
            new Course
            {
                Title = "C# для начинающих",
                Description = "Основы C# и .NET",
                Price = 4999,
                DurationInHours = 20,
                Category = (from category in context.Categories
                           where category.Name == "Программирование"
                           select category).Single(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                ImageUrl = "/images/csharp.jpg"
            },
            new Course
            {
                Title = "Веб-дизайн с нуля",
                Description = "UI/UX для новичков",
                Price = 3999,
                DurationInHours = 15,
                Category = (from category in context.Categories
                           where category.Name == "Дизайн"
                           select category).Single(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                ImageUrl = "/images/design.jpg"
            }
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            // add roles
            var roles = new UserRole[]
            {
                new UserRole { Name = "admin" },
                new UserRole { Name = "manager" }
            };
            context.UserRoles.AddRange(roles);
            context.SaveChanges();

            // add users
            var users = new User[]
            {
                new User
                {
                    Name = "root",
                    Email = "testmail@yandex.ru",
                    PasswordHash = "qwerty12345",
                    Role = (from role in roles
                            where role.Name == "admin"
                            select role).Single()
                },
                new User
                {
                    Name = "manager1",
                    Email = "manager1@yandex.ru",
                    PasswordHash = "ytrewq54321",
                    Role = (from role in roles
                            where role.Name == "manager"
                            select role).Single()
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
