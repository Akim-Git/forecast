using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ForecastContext : DbContext
    {
        public DbSet<Forecast> Forecasts { get; set; }

        public ForecastContext(DbContextOptions options) : base(options) 
        {
        
        }
        
    }
}
