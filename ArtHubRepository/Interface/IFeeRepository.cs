using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface IFeeRepository : IBaseRepository<Fee> 
    {
        Fee? GetFeeByArtistEmail(string email);
        double GetFeeByCreatorEmail(string creatorEmail);

        Fee GetFullFeeByCreatorEmail(string creatorEmail);
    }
}
