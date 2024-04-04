using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Interface
{
    public interface ISystemConfigService
    {

        public  Task<double> GetCommisionRateAsync();

        public Task<bool> UpdateCommisionRateAsync(double newCommisionRate);

    }


}
