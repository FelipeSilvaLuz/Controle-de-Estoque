using Estoque.Domain.Interfaces;
using Estoque.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Estoque.EFData.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntidade
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbset;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = dbContext.Set<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Delete(object[] chavePrimaria)
        {
            var entity = GetById(chavePrimaria);
            _dbset.Remove(entity);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _dbset.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbset.AsNoTracking();
        }

        public virtual TEntity GetById(object[] chavePrimaria)
        {
            return _dbset.Find(chavePrimaria);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _dbset.Update(entity);
        }
    }
}
