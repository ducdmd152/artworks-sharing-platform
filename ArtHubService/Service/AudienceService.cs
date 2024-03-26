using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Payload;
using ArtHubDAO.Interface;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Service
{
    public class AudienceService : IAudienceService
    {
        private readonly IDapperQueryService dapperQueryService;
        private readonly IUnitOfWork unitOfWork;

        public AudienceService(IDapperQueryService dapperQueryService, IUnitOfWork unitOfWork)
        {
            this.dapperQueryService = dapperQueryService;
            this.unitOfWork = unitOfWork;
        }

        public PageResult<SelectCreatorDTO> GetSubcribingCreators(string audienceEmail, int pageIndex = 1, int pageSize = 12)
        {
            try
            {
                var list = this.dapperQueryService
                    .Select<SelectCreatorDTO>(QueryName.SelectSubcribingCreatorList,
                    new
                    {
                        AudienceEmail = audienceEmail,
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                    });

                var result = new PageResult<SelectCreatorDTO>
                {
                    PageData = list.ToList(),
                    PageInfo = new PageInfo
                    {
                        PageNum = pageIndex,
                        PageSize = pageSize,
                        TotalPages = list.First().TotalPages,
                        TotalItems = list.First().TotalItems,
                    }
                };
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetSubcribingCreators || Exception...");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
