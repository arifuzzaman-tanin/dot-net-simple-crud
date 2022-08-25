using System.Linq.Expressions;

namespace Repository.RepositoryPattern
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Find(object predicate);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> Exist(object predicate);
        T Update(T entity);
        T Add(T entity);
        int AddRange(IList<T> entity);
        int Delete(object predicate);
        int DeleteRange(IList<T> entity);
    }
}
