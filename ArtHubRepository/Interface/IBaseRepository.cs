using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Update(TEntity entity);

        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
