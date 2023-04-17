using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVCAppSystem.Models;

namespace MVCAppSystem.DbContexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       
        public DbSet<Login> Logins { get; set; }  
        public DbSet<StaffRegistration> staffRegistrations { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<CustomerRegistration> customerRegisters { get; set; }
        public DbSet<CustomerHistory> customerHistories { get; set; }
    }
}
