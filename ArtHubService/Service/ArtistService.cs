using ArtHubBO.DTO;
using ArtHubDAO.Interface;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using Newtonsoft.Json;

namespace ArtHubService.Service;

public class ArtistService : IArtistService
{
    private readonly IDapperQueryService dapperQueryService;
    private readonly IUnitOfWork unitOfWork;

    public ArtistService(IDapperQueryService dapperQueryService, IUnitOfWork unitOfWork)
    {
        this.dapperQueryService = dapperQueryService;
        this.unitOfWork = unitOfWork;
    }

    public ArtistDataDto GetArtistInforSummaryByCondition()
    {
        var listPost = dapperQueryService.Query<ArtistDataQueryDto>(QueryName.GetArtistInfoByCondition);       
        return ArtistDataQueryDtoToArtistDataDto(listPost.First());
    }

    private ArtistDataDto ArtistDataQueryDtoToArtistDataDto(ArtistDataQueryDto artistDataQueryDto)
    {
        return new ArtistDataDto
        {
            Email = artistDataQueryDto.Email,
            Avatar = artistDataQueryDto.Avatar,
            ArtistName = artistDataQueryDto.ArtistName,
            Bio = artistDataQueryDto.Bio,
            TotalSubscriber = artistDataQueryDto.TotalSubscriber,
            TotalReact = artistDataQueryDto.TotalReact,
            TotalView = artistDataQueryDto.TotalView,
            TotalBookmark = artistDataQueryDto.TotalBookmark,
            PostDetailDtos = JsonConvert.DeserializeObject<List<PostDetailDto>>(artistDataQueryDto.PostDetail ?? "[]")!
        };
    }
}
