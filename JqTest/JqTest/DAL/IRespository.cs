using System;
using System.Linq;
using System.Linq.Expressions;

namespace JqTest.DAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All();
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        void Delete(TEntity entity);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void AddChild(TEntity entity);
    }
}