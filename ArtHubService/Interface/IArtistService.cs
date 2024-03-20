using ArtHubBO.DTO;
using ArtHubBO.Payload;

namespace ArtHubService.Interface;

public interface IArtistService
{
    Task<PageResult<ArtistDataDto>> GetArtistInforSummaryByCondition(SearchPayload<ArtistSearchConditionDto> searchPayload);
}
