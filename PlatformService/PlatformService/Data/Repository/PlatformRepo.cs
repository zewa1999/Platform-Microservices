using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PlatformService.Data.Repository;

public class PlatformRepo<T> : IPlatformRepo<T>, IRepo<T>
    where T : class
{

    private readonly AppDbContext _ctx;

    public PlatformRepo(AppDbContext context)
    {
        _ctx = context;
    }
    public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "")
    {
        try
        {
            var databaseSet = _ctx.Set<T>();

            IQueryable<T> query = databaseSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        catch (Exception ex)
        {
            //this.Logger.Error(ex.Message + "The query will return an empty list!");
        }

        return new List<T>();
    }

    /// <summary>
    /// Inserts the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public virtual bool Insert(T? entity)
    {
        try
        {
            if (entity != null)
            {
                var databaseSet = _ctx.Set<T>();
                databaseSet.Add(entity);
                _ctx.SaveChanges();
            }
            else
            {
                // logger...
            }
        }
        catch (Exception ex)
        {
            // Logger.Error(ex.Message + ex.InnerException + "The INSERT could not been made!");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Updates the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public virtual bool Update(T item)
    {
        try
        {
            var databaseSet = this._ctx.Set<T>();
            databaseSet.Attach(item);
            _ctx.Entry(item).State = EntityState.Modified;

            _ctx.SaveChanges();
        }
        catch (Exception ex)
        {
            //  Logger.Error(ex.Message + ex.InnerException + "The UPDATE could not been made!");
            return false;
        }

        return true;
    }
}
