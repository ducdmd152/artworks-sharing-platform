using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface ISystemConfigRepository : IBaseRepository<SystemConfig> 
    {
        public  Task<SystemConfig> GetSystemConfig();

     //   public Task UpdateSystemConfigAsync(SystemConfig systemConfig);


    }
}
