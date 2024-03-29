using ArtHubBO.Enum;

namespace ArtHubService.Interface;

public interface IPaypalService
{
    Task<string> CreateSubscription(string audienceEmail, string creatorEmail);
    Task<Result> CancelSubscriptionAsync(string accEmail, string? creatorEmail, string reason);
}