using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
        public DbSet<Employee> Employees { get; set; }
    }
}
