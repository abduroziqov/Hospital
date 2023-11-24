using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext()
        {

        }
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
