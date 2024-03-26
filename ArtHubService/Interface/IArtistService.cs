using ArtHubBO.DTO;
using ArtHubBO.Payload;

namespace ArtHubService.Interface;

public interface IArtistService
{
    Task<PageResult<ArtistDataDto>> GetArtistInforSummaryByCondition(SearchPayload<ArtistSearchConditionDto> searchPayload);
    PageResult<SelectCreatorDTO> GetTopCreators(string audienceEmail, int pageIndex, int pageSize);
    SelectCreatorDTO GetCreatorByEmail(string creatorEmail, string audienceEmail);
}
