using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
