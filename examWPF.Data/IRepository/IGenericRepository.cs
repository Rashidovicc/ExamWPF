using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace examWPF.Data.IRepository
{
    public interface IGenericRepository<TSource> where TSource : class
    {
        Task<TSource> CreateAsync(TSource source);
        Task<bool> DeleteAsync(Expression<Func<TSource, bool>> predicate);
        Task<TSource> GetAsync(Expression<Func<TSource, bool>> predicate);
        IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> predicate = null);
        Task<TSource> UpdateAsync(TSource source);
        Task SaveAsync();
    }
}