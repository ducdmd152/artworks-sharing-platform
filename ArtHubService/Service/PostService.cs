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

    public PostService(IPostRepository postRepository, IDapperQueryService dapperQueryService, IUnitOfWork unitOfWork)
    {
        this.postRepository = postRepository;
        this.dapperQueryService = dapperQueryService;
        this.unitOfWork = unitOfWork;        
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

    public async Task<bool> CreateNewPost(Post post)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await postRepository.AddAsync(post).ConfigureAwait(false);            
            await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            return true;
        }
        catch (Exception ex)
        {               
            unitOfWork.RollbackTransaction();
        }
        return false;
    }

    public async Task<Post> UpdatePost(PostUpdateDto post)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            var dataUpdate = postRepository.Get(post.PostId);
            if (post.Images != null && post.Images.First().ImageUrl != null)
            {
                dataUpdate.Images.First().ImageUrl = post.Images.First().ImageUrl;
            }            
            dataUpdate.Title = post.Title;
            dataUpdate.Description = post.Description;
            if (post.PostCategories != null)
            {
                dataUpdate.PostCategories = post.PostCategories;
            }
            dataUpdate.Scope = post.Scope;
            var updatedPost = postRepository.Update(dataUpdate);
            await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            return updatedPost;
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
        }
        return null;
    }
}