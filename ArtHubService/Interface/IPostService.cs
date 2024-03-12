using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;

namespace ArtHubService.Interface;

public interface IPostService
{
   Task< IEnumerable<PostManagementItem>> GetListPostOrderByDate(SearchArtworkManagementConditionDto searchCondition);
    
    List<Post> GetAllPostBySearchCondition(SearchPayload<PostSearchConditionDto> searchPayload);

    List<Post> TestPostCategory();
}