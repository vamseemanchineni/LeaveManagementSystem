using LeaveManagementSystem.Web.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "cb78847a-e1c8-421d-aebe-e69caa254b46",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "641f6ede-4466-4b4e-8f01-c1a28602da86",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                 new IdentityRole
                 {
                     Id = "7252c346-8257-47fe-b91a-ecf35b3f7303",
                     Name = "Administrator",
                     NormalizedName = "ADMINISTRATOR"
                 });
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id= "3e4d0ff0-8da2-4255-945e-71883be3e60b",
                Email="admin@localhost.com",
                NormalizedEmail="ADMIN@LOCALHOST.COM",
                NormalizedUserName= "ADMIN@LOCALHOST.COM",
                UserName= "admin@localhost.com",
                PasswordHash=hasher.HashPassword(null,"P@ssword1"),
                EmailConfirmed=true,
                FirstName = "Default",
                LastName = "Admin",
                DateOfBirth = new DateOnly(1950,12,01)
            });
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId= "7252c346-8257-47fe-b91a-ecf35b3f7303",
                    UserId= "3e4d0ff0-8da2-4255-945e-71883be3e60b"
                });
            builder.ApplyConfiguration(new LeaveRequestStatusConfiguration());
        }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
