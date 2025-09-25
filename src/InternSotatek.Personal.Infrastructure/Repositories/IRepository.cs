using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetByIdAsync(TPrimaryKey id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteByIdAsync(TPrimaryKey id);
        Task DeleteByIdAsync(TPrimaryKey id, CancellationToken cancellationToken);
    }
}
