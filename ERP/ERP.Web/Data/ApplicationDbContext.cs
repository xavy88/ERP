using ERP.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.Web.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
    }
}
