using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Identity.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "44caf514-ddb5-418f-9c15-3708e60cb89f",
                    UserId = "a5540e34-9344-4894-88f0-05f2182e60d7"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "9df6d6a8-93f7-4ed0-af5b-c27b03c80837",
                    UserId = "00fabaf2-1109-4b37-bc65-aac6cdf24638"
                });
        }
    }
}
