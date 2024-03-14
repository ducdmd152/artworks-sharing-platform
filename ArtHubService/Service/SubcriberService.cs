using ArtHubRepository.Interface;
using ArtHubService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Service
{
    public class SubcriberService : ISubcribersService
    {
        private readonly ISubscriberRepository _subcribersRepository;

        public SubcriberService(ISubscriberRepository subcribersRepository)
        {
            _subcribersRepository = subcribersRepository;
        }

        public int GetTotalSubscribers()
        {
           
            return _subcribersRepository.GetTotalSubscribers();
        }
    }
}
