using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Models.Domain;

namespace MVC_CRUD.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees{ get; set; }
    }
}
