using ERP.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
    }
}
