using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;

namespace ArtHubRepository.Repository;

public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
{
    public ArtistRepository(IBaseDAO<Artist> baseDAO) : base(baseDAO)
    { 
    }

    public Artist GetArtistByArtistEmail(string email)
    {
        return this.DbSet.First(at => at.Email == email);
    }
}
