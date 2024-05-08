using CityDistanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CityDistanceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
    }
}
