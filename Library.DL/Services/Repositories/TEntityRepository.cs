using Library.DL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DL.Services.Repositories
{
    public class TEntityRepository<TEntity> : ITEntityRepository<TEntity> where TEntity : class
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly DbSet<TEntity> _dbSet;

        public TEntityRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
            _dbSet = dataBaseContext.Set<TEntity>();
        }
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            _dataBaseContext.SaveChanges();
            return entity;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _dataBaseContext.SaveChanges();
            return true;
        }

        public virtual async Task<IEnumerable<TEntity>> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual Task<IEnumerable<TEntity>> Get(int page, int pageSize, string pole, string sort)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> Get(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> Update(Guid id, TEntity entity)
        {
            _dataBaseContext.Entry(entity).State = EntityState.Modified;
            _dataBaseContext.SaveChanges();
            return entity;
        }
    }
}