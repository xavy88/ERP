using ERP.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERP.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DayOff> DaysOff { get; set; }
        public DbSet<Service> Services{ get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskAssigment> TaskAssigment { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<JobOpening> JobOpenings { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<PayRollOrder> PayRollOrders { get; set; }
        public DbSet<PayRollDetail> PayRollDetails { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ClientAssigment> ClientAssigments { get; set; }


    }
}
