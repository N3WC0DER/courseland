using courseland.Server;
using courseland.Server.Models;
using courseland.Server.Repositories;
using courseland.Server.Services;
using Microsoft.EntityFrameworkCore;
using MimeKit;

var configuration = new WebApplicationOptions() { WebRootPath = "../courseland.client/", Args = args };

var builder = WebApplication.CreateBuilder(configuration);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddLogging();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

// Repositories
builder.Services.AddScoped<IRepository<User>, BaseRepository<User>>();
builder.Services.AddScoped<IRepository<UserRole>, BaseRepository<UserRole>>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

var emailConfiguration = builder.Configuration.GetRequiredSection("Email");

builder.Services.AddSingleton(services =>
    new EmailService(
        services.GetRequiredService<ILogger<EmailService>>(),
        new MailboxAddress(emailConfiguration["Name"], emailConfiguration["Address"]),
        emailConfiguration
    )
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ASPNETCORE_HTTPS_PORT
// ASPNETCORE_URLS
// TODO: replace to env variables!!!

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostOrigin",
        builder => builder.WithOrigins("https://localhost:15035")
                            .AllowAnyHeader()
                            .AllowAnyMethod());
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowLocalhostOrigin");

app.MapControllers();

app.MapFallbackToFile("/index.html");

// mock db data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationContext>();
        MockDbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error when filling the database with initial data");
    }
}

app.Run();