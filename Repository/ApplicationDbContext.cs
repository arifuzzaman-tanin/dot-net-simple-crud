using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseSerialColumns();
        }

        public DbSet<AdmittedStudent> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
