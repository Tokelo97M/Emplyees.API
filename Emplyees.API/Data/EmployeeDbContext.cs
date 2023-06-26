using Emplyees.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Emplyees.API.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Employee> Employees { get; set; }
    }
}
