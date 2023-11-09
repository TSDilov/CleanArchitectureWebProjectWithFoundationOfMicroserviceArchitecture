using TaskManager.Infrastructure.Dtos;

namespace TaskManager.Infrastructure.Contracts
{
    public interface ITaskManagerService
    {
        public Task<List<UserTaskDto>> GetAllTasks(int page, int itemsPerPage, CancellationToken cancellationToken);

        public Task<UserTaskDto> GetById(int id, CancellationToken cancellationToken);

        public Task<int> Create(CreateUserTaskDto createDto, CancellationToken cancellationToken);

        public Task<bool> Edit(EditUserTaskDto createDto, CancellationToken cancellationToken);

        public Task<bool> Delete(int id, CancellationToken cancellationToken);

        public Task<int> GetCount(CancellationToken cancellationToken);
    }
}
