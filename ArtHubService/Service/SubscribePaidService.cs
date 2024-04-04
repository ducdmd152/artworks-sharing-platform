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
    private readonly IArtistRepository artistRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IFeeRepository feeRepository;

    public SubscribePaidService(ISubscriberRepository subscriberRepository, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IFeeRepository feeRepository, IArtistRepository artistRepository)
    {
        this.subscriberRepository = subscriberRepository;
        this.transactionRepository = transactionRepository;
        this.unitOfWork = unitOfWork;
        this.feeRepository = feeRepository;
        this.artistRepository = artistRepository;
    }

    public async Task<Result> SubscribePaidAsync(string subscriptionId, string audienceEmail, string artistEmail)
    {
        try
        {
            await this.unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            bool checkAlreadyPaid = this.subscriberRepository.CheckAreadyPaid(audienceEmail, artistEmail);
            if (!checkAlreadyPaid)
            {
                // increase sub
                var artist = this.artistRepository.GetArtistByArtistEmail(artistEmail);
                artist.TotalSubscribe += 1;
                this.artistRepository.Update(artist);

                var subAvail = this.subscriberRepository.GetAvaiableSubcriber(audienceEmail, artistEmail);
                if (subAvail != null)
                {
                    subAvail.Status = (int)SubscriberStatus.Subscribed;
                    this.subscriberRepository.Update(subAvail);
                }
                else
                {
                    

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
                        SubscriptionPaypalId = subscriptionId,
                        FeeId = fee.FeeId,
                        Subscriber = sub
                    };

                    await this.transactionRepository.AddAsync(tran).ConfigureAwait(false);
                }
            }
            
            await this.unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            return Result.Ok;
        }
        catch (Exception e)
        {
            this.unitOfWork.RollbackTransaction();
            return Result.Error;
        }
    }

    public async Task<Result> UnSubAsync(string creatorEmail, string accEmail)
    {
        try
        {
            await this.unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            var artist = this.artistRepository.GetArtistByArtistEmail(creatorEmail);
            artist.TotalSubscribe -= 1;

            var sub = this.subscriberRepository.GetSubscriber(accEmail, creatorEmail);
            sub.Status = (int)SubscriberStatus.Unsub;

            this.subscriberRepository.Update(sub);
            this.artistRepository.Update(artist);

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