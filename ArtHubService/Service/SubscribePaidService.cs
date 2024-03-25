using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class SubscribePaidService : ISubscribePaidService
{
    private readonly ISubscriberRepository subscriberRepository;
    private readonly ITransactionRepository transactionRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IFeeRepository feeRepository;

    public SubscribePaidService(ISubscriberRepository subscriberRepository, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IFeeRepository feeRepository)
    {
        this.subscriberRepository = subscriberRepository;
        this.transactionRepository = transactionRepository;
        this.unitOfWork = unitOfWork;
        this.feeRepository = feeRepository;
    }

    public async Task<Result> SubscribePaidAsync(string audienceEmail, string artistEmail)
    {
        try
        {
            await this.unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            var sub = new Subscriber
            {
                EmailUser = audienceEmail,
                EmailArtist = artistEmail,
                Status = (int)SubscriberStatus.Subscribed,
                ExpiredDate = DateTime.Now.AddMonths(1),
            };

            var fee = this.feeRepository.GetFullFeeByCreatorEmail(artistEmail);
            var tran = new Transaction
            {
                Amount = fee.Amount,
                Status = (int)TransactionStatus.Paid,
                Type = TransactionType.Paypal.GetDescription(),
                FeeId = fee.FeeId,
                Subscriber = sub
            };

            await this.transactionRepository.AddAsync(tran).ConfigureAwait(false);

            await this.unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            return Result.Ok;
        }
        catch (Exception e)
        {
            this.unitOfWork.RollbackTransaction();
            return Result.Error;
        }
    }
}