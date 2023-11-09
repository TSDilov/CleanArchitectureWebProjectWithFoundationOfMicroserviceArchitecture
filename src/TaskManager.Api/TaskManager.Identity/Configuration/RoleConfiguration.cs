using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Identity.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "44caf514-ddb5-418f-9c15-3708e60cb89f",
                    Name = RoleConstants.User,
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "9df6d6a8-93f7-4ed0-af5b-c27b03c80837",
                    Name = RoleConstants.Administrator,
                    NormalizedName = "ADMINISTRATOR"
                });
        }
    }
}
