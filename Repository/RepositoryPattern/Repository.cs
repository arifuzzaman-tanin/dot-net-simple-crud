using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.RepositoryPattern
{
    public class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public virtual IQueryable<T> GetAllBy(Expression<Func<T, bool>> predicate)
        {
            return entities.Where(predicate).Select(x => x).AsQueryable();
        }

        public virtual T Find(object predicate)
        {
            return entities.Find(predicate);
        }

        public virtual T Add(T entity)
        {
            entities.Add(entity);
            return _applicationDbContext.SaveChanges() != 0 ? entity : null;
        }

        public virtual int AddRange(IList<T> entity)
        {
            entities.AddRange(entity);
            return _applicationDbContext.SaveChanges();
        }

        public virtual int Delete(object predicate)
        {
            T entity = entities.Find(predicate);
            entities.Remove(entity);
            return _applicationDbContext.SaveChanges();
        }

        public virtual int DeleteRange(IList<T> entity)
        {
            entities.RemoveRange(entity);
            return _applicationDbContext.SaveChanges();
        }

        public IQueryable<T> Exist(object predicate)
        {
            return entities.Where(x => x.Equals(predicate));
        }

        public virtual T Update(T entity)
        {
            entities.Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            return _applicationDbContext.SaveChanges() != 0 ? entity : null;
        }

        public void Dispose() => _applicationDbContext.Dispose();
    }
}
