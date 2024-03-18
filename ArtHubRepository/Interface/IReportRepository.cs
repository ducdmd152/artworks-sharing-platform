using ArtHubBO.Entities;

namespace ArtHubRepository.Interface
{
    public interface IReportRepository : IBaseRepository<Report> 
    {
        Report GetReportId(int reportModeReportId);
    }
}
