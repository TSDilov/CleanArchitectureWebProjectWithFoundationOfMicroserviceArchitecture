using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Infrastructure;

namespace TaskManager.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TaskManagerDbContext dbContext;

        public GenericRepository(TaskManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<T> Add(T entity)
        {
            await this.dbContext.AddAsync(entity);
            await this.dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            this.dbContext.Set<T>().Remove(entity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Get(int id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyCollection<T>> GetAll()
        {
            return await this.dbContext.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
