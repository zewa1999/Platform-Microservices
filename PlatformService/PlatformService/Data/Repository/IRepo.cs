using System.Linq.Expressions;

namespace PlatformService.Data.Repository;

public interface IRepo<T>
{
        IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");

        bool Insert(T entity);

        bool Update(T item);
}
