using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;

namespace ArtHubRepository.Interface
{
    public interface IPostRepository : IBaseRepository<Post> 
    {
        Task<List<Post>> GetAllPostBySearchConditionAsync(SearchPayload<PostSearchConditionDto> searchPayload);
        List<Post> GetAllPost();
        Post Get(int id);
    }
}
