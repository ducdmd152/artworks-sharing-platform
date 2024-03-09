using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;

namespace ArtHubService.Interface;

public interface IPostService
{
    List<Post> GetAllPostBySearchCondition(SearchPayload<PostSearchConditionDto> searchPayload);
}
