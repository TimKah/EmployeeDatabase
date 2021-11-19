using Employee.Database.DAO;
using Microsoft.EntityFrameworkCore;

namespace Employee.Database.Contexts
{
    public class EmployeesRepositoryContext : DbContext
    {
        public DbSet<EmployeeDAO> Employees { get; set; }

        public EmployeesRepositoryContext(DbContextOptions<EmployeesRepositoryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasPostgresExtension("uuid-ossp");
        }
    }
}
