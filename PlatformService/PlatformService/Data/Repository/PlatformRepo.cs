using Microsoft.EntityFrameworkCore;
using PlatformService.Models;
using System.Linq.Expressions;

namespace PlatformService.Data.Repository;

public class PlatformRepo : IPlatformRepo<Platform>
{
    private readonly AppDbContext _ctx;

    public PlatformRepo(AppDbContext context)
    {
        _ctx = context;
    }

    public virtual IEnumerable<Platform> Get(
            Expression<Func<Platform, bool>>? filter = null,
            Func<IQueryable<Platform>, IOrderedQueryable<Platform>>? orderBy = null,
            string includeProperties = "")
    {
        try
        {
            var databaseSet = _ctx.Set<Platform>();

            IQueryable<Platform> query = databaseSet;

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

        return new List<Platform>();
    }

    /// <summary>
    /// Inserts the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public virtual bool Insert(Platform? entity)
    {
        try
        {
            if (entity != null)
            {
                var databaseSet = _ctx.Set<Platform>();
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
    public virtual bool Update(Platform item)
    {
        try
        {
            var databaseSet = this._ctx.Set<Platform>();
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