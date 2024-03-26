using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface IFeeRepository : IBaseRepository<Fee> 
    {
        double GetFeeByCreatorEmail(string creatorEmail);

        Fee GetFullFeeByCreatorEmail(string creatorEmail);
    }
}
