using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtHubRepository.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseDAO<TEntity> baseDAO;

        public BaseRepository(IBaseDAO<TEntity> baseDAO)
        {
            this.baseDAO = baseDAO;
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return this.baseDAO.DbSet;
            }
        }

        public async Task AddAsync(TEntity entity)
        => await baseDAO.AddAsync(entity).ConfigureAwait(false);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        => await baseDAO.AddRangeAsync(entities).ConfigureAwait(false);

        public void Remove(TEntity entity)
        => baseDAO.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities)
        => baseDAO.RemoveRange(entities);

        public TEntity Update(TEntity entity)
        => baseDAO.Update(entity);

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        => baseDAO.UpdateRange(entities);
    }
}
