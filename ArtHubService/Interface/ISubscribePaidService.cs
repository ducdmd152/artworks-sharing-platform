using ArtHubBO.Enum;

namespace ArtHubService.Interface;

public interface ISubscribePaidService
{
    Task<Result> SubscribePaidAsync(string audienceEmail, string artistEmail);
}