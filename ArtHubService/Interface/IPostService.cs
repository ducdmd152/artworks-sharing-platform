using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;

namespace ArtHubService.Interface;

public interface IPostService
{
   Task< IEnumerable<PostManagementItem>> GetListPostOrderByDate(SearchArtworkManagementConditionDto searchCondition);
    
    Task<List<Post>> GetAllPostBySearchConditionAsync(SearchPayload<PostSearchConditionDto> searchPayload);

    List<Post> TestPostCategory();

    Post Get(int id);

    Task<bool> CreateNewPost(Post post, List<PostCategory> postCategories, Image image);    
}