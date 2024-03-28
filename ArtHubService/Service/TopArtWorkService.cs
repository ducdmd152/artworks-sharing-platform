using ArtHubBO.DTO;
using ArtHubBO.Payload;
using ArtHubDAO.Interface;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Service
{
    public class TopArtWorkService : ITopArtWorkService
    {
       
        private readonly IDapperQueryService dapperQueryService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<AccountService> logger;

        public TopArtWorkService( IUnitOfWork unitOfWork, IDapperQueryService dapperQueryService, ILogger<AccountService> logger)
        {
            
            this.unitOfWork = unitOfWork;
            this.dapperQueryService = dapperQueryService;
            this.logger = logger;
        }
        public async Task<PageResult<TopArtWorkDTO>> GetArtWork(
            SearchArtWorkDTO search)
        {
            try
            {
                var listPost = this.dapperQueryService
                    .Select<TopArtWorkDTO>(QueryName.TopArtWorkListQuery, search);
                var result = new PageResult<TopArtWorkDTO>
                {
                    PageData = listPost.ToList(),
                    PageInfo = new PageInfo
                    {
                        PageNum = search.PageNumber,
                        PageSize = search.PageSize,
                        TotalPages = listPost.First().TotalPages,
                        TotalItems = listPost.First().TotalItems,
                    }
                };
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

