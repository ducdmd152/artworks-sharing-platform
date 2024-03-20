using ArtHubBO.DTO;
using ArtHubBO.Payload;
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

    public async Task<PageResult<ArtistDataDto>> GetArtistInforSummaryByCondition(SearchPayload<ArtistSearchConditionDto> searchPayload)
    {
        PageResult<ArtistDataDto> pageResult = new PageResult<ArtistDataDto>();        
        var postQuery = await dapperQueryService.SingleOrDefaultAsync<ArtistDataQueryDto>(QueryName.GetArtistInfoByCondition, new
        {
            IsGetDataPost = searchPayload.SearchCondition.IsGetDataPost,
            Email = searchPayload.SearchCondition.Email,
            PostScope = string.Join(",", searchPayload.SearchCondition.PostScope?.Select(x => x.ToString()) ?? Array.Empty<string>()),
            PostStatus = string.Join(",", searchPayload.SearchCondition.PostStatus?.Select(x => x.ToString()) ?? Array.Empty<string>()),
            IsOrderByReact = searchPayload.SearchCondition.IsOrderByReact,
            IsOrderByView = searchPayload.SearchCondition.IsOrderByView,
            IsOrderByTitle = searchPayload.SearchCondition.IsOrderByTitle,
            IsOrderAsc = searchPayload.SearchCondition.IsOrderAsc,
            PageNum = searchPayload.PageInfo.PageNum,
            PageSize = searchPayload.PageInfo.PageSize,
            AccountStatus = searchPayload.SearchCondition.AccountStatus,
            AccountIsEnable = searchPayload.SearchCondition.AccountIsEnable,
        });        
        return pageResult.Build(searchPayload.PageInfo, postQuery.TotalPostCount, new List<ArtistDataDto>() { ArtistDataQueryDtoToArtistDataDto(postQuery) });
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
            PostDetailDtos = JsonConvert.DeserializeObject<List<PostDetailDto>>(artistDataQueryDto.PostDetail ?? "[]")!,
            TotalPostCount = artistDataQueryDto.TotalPostCount,
        };
    }
}
