using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BringIt.Restaurant.Infrastrucutre.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeExpressions);
    }
}
