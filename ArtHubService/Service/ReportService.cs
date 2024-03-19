using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubDAO.Interface;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using Microsoft.Extensions.Logging;

namespace ArtHubService.Service;

public class ReportService : IReportService
{
    private readonly IDapperQueryService dapperQueryService;
    private readonly IReportRepository reportRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<ReportService> logger;

    public ReportService(IDapperQueryService dapperQueryService, IReportRepository reportRepository, IUnitOfWork unitOfWork, IAccountRepository accountRepository, ILogger<ReportService> logger)
    {
        this.dapperQueryService = dapperQueryService;
        this.reportRepository = reportRepository;
        this.unitOfWork = unitOfWork;
        this.accountRepository = accountRepository;
        this.logger = logger;
    }


    public PageResult<ReportManagementItem> GetReportList(SearchReportManagementConditionDto searchCondition)
    {
        var list = this.dapperQueryService.Select<ReportManagementItem>(QueryName.GetListReportByDate,
            searchCondition);

        if (list != default && list.Count() > 0)
        {
            var result = new PageResult<ReportManagementItem>
            {
                PageInfo = new PageInfo
                {
                    PageNum = searchCondition.PageNumber,
                    PageSize = searchCondition.PageSize,
                    TotalItems = list.First().TotalItems,
                    TotalPages = list.First().TotalPages
                },
                PageData = list.ToList(),
            };

            return result;
        }
       
        
        return new PageResult<ReportManagementItem>();
    }

    public async Task<Result> SkipOrBanPostAsync(int reportModeReportId, int reportModeMode)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            var report = this.reportRepository.GetReportId(reportModeReportId);
            if (report != default)
            {
                if (reportModeMode == 1)
                {
                    report.Status = (int)ReportStatus.Reviewed;
                }else if (reportModeMode == -1)
                {
                    report.Status = (int)ReportStatus.Reviewed;
                    report.Post.Status = (int)PostStatus.Repending;
                }else if (reportModeMode == -2)
                {
                    var account = this.accountRepository.GetAccount(report.Post.ArtistEmail);
                    if (account != default)
                    {
                        account.Enabled = false;
                        this.accountRepository.Update(account);
                    }
                    report.Status = (int)ReportStatus.Reviewed;
                    report.Post.Status = (int)PostStatus.Repending;
                }
            }

            this.reportRepository.Update(report);
            await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            return Result.Ok;
        }
        catch (Exception e)
        {
            unitOfWork.RollbackTransaction();
            return Result.Error;
        }
    }

    public async Task<Result> Register(int reportDetailPostId, string reportDetailReason, string getEmailAccountLogin)
    {
        try
        {
            var report = new Report
            {
                Reason = reportDetailReason,
                Status = (int)ReportStatus.Pending,
                PostId = reportDetailPostId,
                ReporterEmail = getEmailAccountLogin
            };
            await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            this.reportRepository.AddAsync(report);
            await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            return Result.Ok;
        }
        catch (Exception ex)
        {
            this.unitOfWork.RollbackTransaction();
            logger.LogWarning("Register new report fail {0}", ex.Message);
            return Result.Ok;
        }
    }
}
