using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IDapperQueryService dapperQueryService;

    public PostService(IPostRepository postRepository, IDapperQueryService dapperQueryService)
    {
        this._postRepository = postRepository;
        this.dapperQueryService = dapperQueryService;
    }

    public async Task<IEnumerable<PostManagementItem>> GetListPostOrderByDate(SearchArtworkManagementConditionDto searchCondition)
    {
        var listPost = await this.dapperQueryService
            .SelectAsync<PostManagementItem>(QueryName.GetListPostOrderByDate, searchCondition).ConfigureAwait(false);
        return listPost;
    }

    public List<Post> GetAllPostBySearchCondition(SearchPayload<PostSearchConditionDto> searchPayload)
    {
        return _postRepository.GetAllPostBySearchCondition(searchPayload);       
    }

    public List<Post> TestPostCategory()
    {
        return _postRepository.GetAllPost();
    }
}