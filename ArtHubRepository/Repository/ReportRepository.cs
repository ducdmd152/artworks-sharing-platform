using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtHubRepository.Repository
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public ReportRepository(IBaseDAO<Report> baseDAO) : base(baseDAO)
        {
        }

        public Report GetReportId(int reportModeReportId)
            => this.DbSet.Include(x => x.Post).FirstOrDefault(x => x.ReportId == reportModeReportId);
    }
}
