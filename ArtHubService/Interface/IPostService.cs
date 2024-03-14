using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;

namespace ArtHubService.Interface;

public interface IPostService
{
   Task<PageResult<PostManagementItem>> GetListPostOrderByDate(SearchArtworkManagementConditionDto searchCondition);
    
    Task<List<Post>> GetAllPostBySearchConditionAsync(SearchPayload<PostSearchConditionDto> searchPayload);

    List<Post> TestPostCategory();

    Post Get(int id);
}