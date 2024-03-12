using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;    
    private readonly IDapperQueryService dapperQueryService;

    public PostService(IPostRepository postRepository, IDapperQueryService dapperQueryService)
    {
        this.postRepository = postRepository;
        this.dapperQueryService = dapperQueryService;
    }

    public async Task<IEnumerable<PostManagementItem>> GetListPostOrderByDate(SearchArtworkManagementConditionDto searchCondition)
    {
            searchCondition = new SearchArtworkManagementConditionDto();
            searchCondition.PageNumber = 1;
            searchCondition.PageSize = 5;
            var listPost = this.dapperQueryService
                .Select<PostManagementItem>(QueryName.GetListPostOrderByDate, searchCondition);
            return listPost;
    }

    public async Task<List<Post>> GetAllPostBySearchConditionAsync(SearchPayload<PostSearchConditionDto> searchPayload)
    {
        return await postRepository.GetAllPostBySearchConditionAsync(searchPayload);
    }

    public Post Get(int id) => postRepository.Get(id);
    public List<Post> TestPostCategory()
    {
        return postRepository.GetAllPost();
    }
}