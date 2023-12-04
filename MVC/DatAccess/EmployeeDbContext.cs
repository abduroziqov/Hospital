using Microsoft.EntityFrameworkCore;
using MVC.Models.DBEntities;

namespace MVC.DatAccess
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
        {
            
        }
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
