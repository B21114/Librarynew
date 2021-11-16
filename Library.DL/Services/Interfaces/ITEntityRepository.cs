using Library.DL.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DL.Services.Interfaces
{
    public interface ITEntityRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get();
        Task<IEnumerable<TEntity>> Get(int page, int pageSize, string pole, string sort);
        Task<TEntity> Get(Guid id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(Guid id, TEntity entity);
        Task<bool> Delete(TEntity entity);
    }
}
