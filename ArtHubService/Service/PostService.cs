using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
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
            
        }
        return false;
    }

    public async Task<Result> UpdateStatusOfPostAsync(int artworkModePostId, int artworkModeMode)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            var post = this.postRepository.GetById(artworkModePostId);
            if (post != default)
            {
                post.Status = artworkModeMode;
            }
            //await postCategoryRepository.AddRangeAsync(postCategories).ConfigureAwait(false);
            await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            return Result.Ok;
        }
        catch (Exception e)
        {
            unitOfWork.RollbackTransaction();
            return Result.Error;
        }
    }
}