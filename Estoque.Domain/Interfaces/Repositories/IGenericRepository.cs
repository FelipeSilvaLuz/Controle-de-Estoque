using System;
using System.Linq;
using System.Linq.Expressions;

namespace Estoque.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntidade
    {
        void Create(TEntity entity);

        void Delete(object[] chavePrimaria);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        TEntity GetById(object[] chavePrimaria);

        void SaveChanges();

        void Update(TEntity entity);
    }
}
