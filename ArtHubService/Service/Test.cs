using ArtHubRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Service
{
    public class Test
    {
        private IAccountRepository accountRepository;

        public Test(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
    }
}
