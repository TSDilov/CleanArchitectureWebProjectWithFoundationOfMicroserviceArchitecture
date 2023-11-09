using TaskManager.Domain;

namespace TaskManager.Application.Contracts.Infrastructure
{
    public interface ITaskRepository : IGenericRepository<UserTask>
    {
        public Task<List<UserTask>> GetByUser(string userId, CancellationToken cancellationToken);
    }
}
