using System.Linq;
using SharedKernel.Helpers;
using SharedKernel.Helpers.Repository;
using System.Data.Entity;
using System.Collections.Generic;

namespace EntityFrameworkConfig
{
    public class Repository : IRepository
    {
        private DbContext _dbContext;

        public Repository(
            DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private DbSet<TEntity> Set<TEntity>()
               where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : class, IAggregateRoot
        {
            return Set<TEntity>().AsQueryable();
        }

        IEnumerable<TEntity> IRepository.GetAll<TEntity>()
        {
            return Set<TEntity>();
        }
    }
}
