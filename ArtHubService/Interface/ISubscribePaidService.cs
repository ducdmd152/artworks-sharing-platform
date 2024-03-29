using ArtHubBO.Enum;

namespace ArtHubService.Interface;

public interface ISubscribePaidService
{
    Task<Result> SubscribePaidAsync(string subscriptionId, string audienceEmail, string artistEmail);
    Task<Result> UnSubAsync(string creatorEmail, string accEmail);
}