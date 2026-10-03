using Microsoft.EntityFrameworkCore;
using ProjectApp.Application.Contracts;
using ProjectApp.Persistence.Context;

namespace ProjectApp.Persistence.Concrete
{
    public class GenericRepository<TEntity>(AppDbContext _context) : IRepository<TEntity> where TEntity : class
    {

        private readonly DbSet<TEntity> _table=_context.Set<TEntity>();

        public async Task CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);

        }

        public void Delete(TEntity entity)
        {
           _table.Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }
    }
}
