using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface IReactionRepository : IBaseRepository<Reaction>
    {
        Reaction GetByCompositeKey(string email, int postId);
    }
}
