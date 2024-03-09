using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;

namespace ArtHubRepository.Interface
{
    public interface IPostRepository : IBaseRepository<Post> 
    {
        List<Post> GetAllPostBySearchCondition(SearchPayload<PostSearchConditionDto> searchPayload);
        List<Post> GetAllPost();
    }
}
