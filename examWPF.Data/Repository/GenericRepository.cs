using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using examWPF.Data.DbContexts;
using examWPF.Data.IRepository;
using Microsoft.EntityFrameworkCore;

namespace examWPF.Data.Repository
{
    #pragma warning disable
    public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
    {
        private readonly ExamDbContexts _dbContext;
        private readonly DbSet<TSource> _dbSet;

        public GenericRepository()
        {
            _dbContext = new ExamDbContexts();
            _dbSet = _dbContext.Set<TSource>();
        }
        public async Task<TSource> CreateAsync(TSource source)
            => (await _dbSet.AddAsync(source)).Entity;


        public async Task<bool> DeleteAsync(Expression<Func<TSource, bool>> predicate)
           {
               var entity = await GetAsync(predicate);

               if (entity is null)
                   return false;

               _dbSet.Remove(entity);
               return true;
            }
       

        public async Task<TSource> GetAsync(Expression<Func<TSource, bool>> predicate)
            => await _dbSet.FirstOrDefaultAsync(predicate);

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> predicate = null)
            => predicate is null ? _dbSet : _dbSet.Where(predicate);

        public async Task<TSource> UpdateAsync(TSource source)
            => _dbSet.Update(source).Entity;

        public async Task SaveAsync()
            =>await _dbContext.SaveChangesAsync();
    }
}