namespace ArtHubService.Interface;

public interface IPaypalService
{
    Task<string> CreateSubscription(string audienceEmail, string creatorEmail);
}