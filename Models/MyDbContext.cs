using Microsoft.EntityFrameworkCore;

namespace Agentic.Models
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> option) : base(option)
        {
        }
        public DbSet<Weatherlog> WeatherLogs { get; set; }
    }
}
