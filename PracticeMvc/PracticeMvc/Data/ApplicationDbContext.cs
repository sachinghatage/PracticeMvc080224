using Microsoft.EntityFrameworkCore;
using PracticeMvc.Models;

namespace PracticeMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeUser> EmployeesUser { get; set; }
    }
}
