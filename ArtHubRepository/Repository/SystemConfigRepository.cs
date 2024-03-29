using Amazon.S3.Model;
using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtHubRepository.Repository
{
    public class SystemConfigRepository : BaseRepository<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(IBaseDAO<SystemConfig> baseDAO) : base(baseDAO)
        {
            
        }
        public async Task<SystemConfig> GetSystemConfig()
        {
            return await this.DbSet.FirstOrDefaultAsync();
        }

      
    }

}

