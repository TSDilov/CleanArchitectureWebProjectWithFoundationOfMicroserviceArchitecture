using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain;

namespace TaskManager.Infrastructure.Configuration.Entities
{
    public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.HasData(
                new UserTask
                {
                    Id = 1,
                    User = "TsvetelinDev91",
                    StartDateTime = DateTime.UtcNow,
                    EndDateTime = DateTime.UtcNow.AddDays(1),
                    Subject = "Seeding data",
                    Description = "Default configuration of the entity",
                }
            );
        }
    }
}
