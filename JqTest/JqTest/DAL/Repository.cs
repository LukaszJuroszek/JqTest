using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace JqTest.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private JqContext _context;

        public Repository(JqContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void AddChild(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public IQueryable<TEntity> All()
        {
            return _context.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}