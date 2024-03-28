using ArtHubBO.Entities;

namespace ArtHubService.Interface;

public interface IFeeService
{
    Fee? GetFeeByArtistEmail(string email);
    Task<Fee?> UpdateAsync(Fee fee);
   double GetFeeSubscribe(string creatorEmail);
}
