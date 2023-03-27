using System.Linq.Expressions;

namespace StoreApp.Data.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T> CreatAsync(T entity);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);


    }
}
