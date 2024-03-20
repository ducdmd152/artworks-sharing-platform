using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class SubscriberService : ISubscriberService
{
    private readonly ISubscriberRepository subscriberRepository;

    public SubscriberService(ISubscriberRepository subscriberRepository)
    {
        this.subscriberRepository = subscriberRepository;
    }
}
