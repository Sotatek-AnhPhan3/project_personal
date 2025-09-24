using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity?> FirstOrDefaultAsync();
        Task<TEntity?> GetByIdAsync(TPrimaryKey id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(TPrimaryKey id);
    }
}
