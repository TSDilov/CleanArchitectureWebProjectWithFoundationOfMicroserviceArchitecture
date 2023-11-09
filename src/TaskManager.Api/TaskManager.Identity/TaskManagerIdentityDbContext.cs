using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Identity.Configuration;
using TaskManager.Identity.Models;

namespace TaskManager.Identity
{
    public class TaskManagerIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public TaskManagerIdentityDbContext(DbContextOptions<TaskManagerIdentityDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }    
}
