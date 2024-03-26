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
using Microsoft.Extensions.Logging;

namespace ArtHubService.Service;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;    
    private readonly IDapperQueryService dapperQueryService;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<PostService> logger;

    public PostService(IPostRepository postRepository, IDapperQueryService dapperQueryService, IUnitOfWork unitOfWork, ILogger<PostService> logger)
    {
        this.postRepository = postRepository;
        this.dapperQueryService = dapperQueryService;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
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

    public async Task<IList<Post>> GetAllPostBySearchConditionForAudienceAsync(SearchPayload<PostAudienceSearchConditionDto> searchPayload)
    {
        return await postRepository.GetAllPostBySearchConditionForAudienceAsync(searchPayload);
    }

    public async Task<PageResult<SelectPostDTO>> GetReactedPostList(string audienceEmail, int pageIndex = 1, int pageSize = 12)
    {
        try
        {
            var list = this.dapperQueryService
                .Select<SelectPostDTO>(QueryName.SelectReactedPostList,
                new
                {
                    AudienceEmail = audienceEmail,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                });

            var result = new PageResult<SelectPostDTO>
            {
                PageData = list.ToList(),
                PageInfo = new PageInfo
                {
                    PageNum = pageIndex,
                    PageSize = pageSize,
                    TotalPages = list.First().TotalPages,
                    TotalItems = list.First().TotalItems,
                }
            };
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine("GetReactedPostList || Exception...");
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<PageResult<SelectPostDTO>> GetBookmarkedPostList(string audienceEmail, int pageIndex = 1, int pageSize = 12)
    {
        try
        {
            var list = this.dapperQueryService
                .Select<SelectPostDTO>(QueryName.SelectBookmarkedPostList,
                new
                {
                    AudienceEmail = audienceEmail,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                });

            var result = new PageResult<SelectPostDTO>
            {
                PageData = list.ToList(),
                PageInfo = new PageInfo
                {
                    PageNum = pageIndex,
                    PageSize = pageSize,
                    TotalPages = list.First().TotalPages,
                    TotalItems = list.First().TotalItems,
                }
            };
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine("GetReactedPostList || Exception...");
            Console.WriteLine(e.Message);
            return null;
        }
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
            logger.LogInformation("Image in post service {0}", post.Images.First().ImageUrl);
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