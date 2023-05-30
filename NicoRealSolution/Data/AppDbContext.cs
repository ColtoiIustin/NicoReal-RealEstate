using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NicoRealSolution.Models;
using NicoRealSolution.Areas.Identity.Data;
using NicoRealSolution.Models;
using Microsoft.AspNetCore.Identity;

namespace NicoRealSolution.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Property> Properties { get; set; }
    }
}
