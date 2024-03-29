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
    private readonly IPostRepository postRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<ReportService> logger;
    private readonly IEmailService emailService;

    public ReportService(IDapperQueryService dapperQueryService, IReportRepository reportRepository, IUnitOfWork unitOfWork, IAccountRepository accountRepository, ILogger<ReportService> logger, IEmailService emailService, IPostRepository postRepository)
    {
        this.dapperQueryService = dapperQueryService;
        this.reportRepository = reportRepository;
        this.unitOfWork = unitOfWork;
        this.accountRepository = accountRepository;
        this.logger = logger;
        this.emailService = emailService;
        this.postRepository = postRepository;
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

    public async Task<Result> SkipOrBanPostAsync(int reportModeReportId, int reportModeMode, string reportModeReason)
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
                    report.Post.Note = reportModeReason;
                }else if (reportModeMode == -2)
                {
                    // send email
                    var post = this.postRepository.GetById(report.PostId);
                    var email = new SendEmailDto
                    {
                        Subject = "Your ArtHub account ban",
                        ToEmail = report.Post.ArtistEmail,
                        Body = @"
                                <html>
                                    <body style='font-family: Arial, sans-serif; color: #333;'>
                                        <div style='margin-bottom: 20px;'>
                                            <img src='https://d28yx6l5j59h9f.cloudfront.net/Artwork/01732ec7-756b-4621-94c1-2da9a8647be0.png' alt='ArtHub Logo' style='display: block; margin: 0 auto;' />
                                        </div>
                                        <p>Hello,</p>
                                        <p>You got banned from our platform. The reason is as follows:</p>
                                        <p>"+reportModeReason+ @"</p>
                                        <p>The post that led to your ban has the following details:</p>
                                        <p>Post ID: "+post.PostId+@"</p>
                                        <p>Link to artwork: "+post.Images.First().ImageUrl +@"</p>
                                        <p>Description: "+post.Description+@"</p>
                                        <p>Please contact this email if you have any questions.</p>
                                    </body>
                                </html>",
                    };
                    this.emailService.SendEmail(email);
                    var account = this.accountRepository.GetAccount(report.Post.ArtistEmail);
                    if (account != default)
                    {
                        account.Enabled = false;
                        this.accountRepository.Update(account);
                    }
                    report.Status = (int)ReportStatus.Reviewed;
                    report.Post.Status = (int)PostStatus.Repending;
                    report.Post.Note = reportModeReason;
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
