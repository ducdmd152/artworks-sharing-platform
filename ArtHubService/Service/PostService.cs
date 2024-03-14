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

    public async Task<PageResult<PostManagementItem>> GetListPostOrderByDate(
        SearchArtworkManagementConditionDto searchCondition)
    {
        try
        {
            var listPost = this.dapperQueryService
                .Select<PostManagementItem>(QueryName.GetListPostOrderByDate, searchCondition);
            var result = new PageResult<PostManagementItem>
            {
                PageData = listPost.ToList(),
                PageInfo = new PageInfo
                {
                    PageNum = searchCondition.PageNumber,
                    PageSize = searchCondition.PageSize,
                    TotalPages = listPost.First().TotalPages,
                    TotalItems = listPost.First().TotalItems,
                }
            };
            return result;
        }
        catch (Exception e)
        {
            return null;
        }
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