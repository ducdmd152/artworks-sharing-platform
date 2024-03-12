using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;
using ArtHubDAO.Data;
using ArtHubDAO.Interface;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubRepository.Repository;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;    
    private readonly IDapperQueryService dapperQueryService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPostCategoryRepository postCategoryRepository;

    public PostService(IPostRepository postRepository, IDapperQueryService dapperQueryService, IUnitOfWork unitOfWork, IPostCategoryRepository postCategoryRepository)
    {
        this.postRepository = postRepository;
        this.dapperQueryService = dapperQueryService;
        this.unitOfWork = unitOfWork;
        this.postCategoryRepository = postCategoryRepository;
    }

    public async Task<IEnumerable<PostManagementItem>> GetListPostOrderByDate(SearchArtworkManagementConditionDto searchCondition)
    {
        var listPost = await this.dapperQueryService
            .SelectAsync<PostManagementItem>(QueryName.GetListPostOrderByDate, searchCondition).ConfigureAwait(false);
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

    public async Task<bool> CreateNewPost(Post post, List<PostCategory> postCategories, Image image)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await postRepository.AddAsync(post).ConfigureAwait(false);
            //await postCategoryRepository.AddRangeAsync(postCategories).ConfigureAwait(false);
            await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            return true;
        }
        catch (Exception ex)
        {               
            unitOfWork.RollbackTransaction();
        }
        return false;
    }
}