using Agentic.Models;
using Agentic.Services;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Agentic.WeatherTask;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add Razor Pages support (if needed for the UI)
builder.Services.AddRazorPages();

// Configure the database context with SQL Server
//builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register HttpClient and WeatherService
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddScoped<WeatherService>();
builder.Services.AddScoped<WeatherTask>();

// Configure Hangfire with in-memory storage (for testing)
// If you want persistent storage, use SQL Server, Redis, or other supported options.
builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage(); // For in-memory storage (ideal for testing)
});
builder.Services.AddHangfireServer(); // Adds the Hangfire server to handle background jobs

// Add Controllers
builder.Services.AddControllers();

// Add OpenAPI (Swagger) for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development mode
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Hangfire dashboard for monitoring and scheduling jobs
app.UseHangfireDashboard("/hangfire"); // The dashboard will be available at /hangfire

app.UseHttpsRedirection();

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Map Razor Pages (if needed for UI)
app.MapRazorPages();

// Initialize Hangfire storage (this is important)
app.UseHangfireDashboard();

// Add recurring jobs to the Hangfire job manager
RecurringJob.AddOrUpdate<WeatherTask>(x => x.FetchWeatherData(), Cron.Hourly); // For every minute job

app.Run();