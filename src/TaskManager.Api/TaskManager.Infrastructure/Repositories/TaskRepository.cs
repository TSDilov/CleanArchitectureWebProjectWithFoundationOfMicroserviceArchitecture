using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Infrastructure;
using TaskManager.Domain;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository : GenericRepository<UserTask>, ITaskRepository
    {
        private readonly TaskManagerDbContext dbContext;

        public TaskRepository(TaskManagerDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<UserTask>> GetByUser(string userId, CancellationToken cancellationToken)
         => this.dbContext.Tasks.Where(x=> x.User == userId).ToListAsync(cancellationToken);
    }
}
