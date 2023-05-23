using Microsoft.EntityFrameworkCore;
using NicoRealSolution.Models;

namespace NicoRealSolution.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Property> Properties { get; set; }
    }
}
