using ASPCRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPCRUD.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
