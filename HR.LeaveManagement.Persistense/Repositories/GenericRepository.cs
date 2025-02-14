using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistense.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LeaveManagementDBContext context;

        public GenericRepository(LeaveManagementDBContext context)
        {
            this.context = context;
        }
        public async Task<T> Add(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<bool> Exists(int Id)
        {
            var entity = await Get(Id);
            return entity != null;
        }
        public async Task<T?> Get(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
