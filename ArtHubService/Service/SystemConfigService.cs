using ArtHubDAO.Data;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Service
{
    
    public class SystemConfigService : ISystemConfigService
    {
       private readonly ISystemConfigRepository _systemConfigRepository;
        private readonly IUnitOfWork unitOfWork;
      

        public SystemConfigService(ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork)
        {
            _systemConfigRepository = systemConfigRepository;
            this.unitOfWork = unitOfWork;
         
        }

        public async Task<double> GetCommisionRateAsync()
        {
            try
            {
                var systemConfig = await _systemConfigRepository.GetSystemConfig();
                return systemConfig.CommisionRate;
            }
            catch (Exception ex)
            {
                
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        public async Task<bool> UpdateCommisionRateAsync(double newCommisionRate)
        {
            try
            {
                 this.unitOfWork.BeginTransactionAsync().ConfigureAwait(false);

                var systemConfig = await _systemConfigRepository.GetSystemConfig().ConfigureAwait(false);
                systemConfig.CommisionRate = newCommisionRate;

                _systemConfigRepository.Update(systemConfig);
                
                this.unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                this.unitOfWork.RollbackTransaction();
                return false;
            }
        }
    }
    
}
