using ArtHubBO.Entities;

namespace ArtHubRepository.Interface;

public interface IArtistRepository : IBaseRepository<Artist> 
{
    Artist GetArtistByArtistEmail(string email);
}
