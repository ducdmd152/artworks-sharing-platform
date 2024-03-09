using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;
using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public List<Post> GetAllPostBySearchCondition(SearchPayload<PostSearchConditionDto> searchPayload)
    {
        return _postRepository.GetAllPostBySearchCondition(searchPayload);       
    }
}
