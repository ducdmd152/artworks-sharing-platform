using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;

namespace ArtHubService.Interface;

public interface IPostService
{
   Task<PageResult<PostManagementItem>> GetListPostOrderByDate(SearchArtworkManagementConditionDto searchCondition);
    
    Task<List<Post>> GetAllPostBySearchConditionAsync(SearchPayload<PostSearchConditionDto> searchPayload);

    List<Post> TestPostCategory();

    Post Get(int id);

    Task<bool> CreateNewPost(Post post);
    
    Task<Post> UpdatePost(PostUpdateDto post);
    
    Task<Result> UpdateStatusOfPostAsync(int artworkModePostId, int artworkModeMode);
    Task<PageResult<SelectPostDTO>> GetReactedPostList(string audienceEmail, int pageIndex = 1, int pageSize = 12);
}