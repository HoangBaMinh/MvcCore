using Microsoft.EntityFrameworkCore;

namespace mvcCore.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts)
        {

        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employees { get; set; }
    } 
}
