using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Contexts;
using StoreApp.Data.IRepositories;
using System.Linq.Expressions;

namespace StoreApp.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext _db;

        private DbSet<T> _dbSet;

        public GenericRepository()
        {
            _db = new AppDbContext();
            _dbSet = _db.Set<T>();
        }


        public async Task<T> CreatAsync(T entity)
        {
            var entry = await _dbSet.AddAsync(entity);

            await _db.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = _dbSet.FirstOrDefault(expression);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            return expression == null ? _dbSet : _dbSet.Where(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var updatedEntity = _dbSet.Update(entity);

            await _db.SaveChangesAsync();

            return updatedEntity.Entity;
        }

    }
}
