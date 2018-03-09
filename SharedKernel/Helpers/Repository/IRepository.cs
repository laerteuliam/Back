using System.Collections.Generic;
using System.Linq;

namespace SharedKernel.Helpers.Repository
{
    public interface IRepository
    {
        IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : class, IAggregateRoot;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IAggregateRoot;
    }
}
