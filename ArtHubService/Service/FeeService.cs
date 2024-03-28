using ArtHubRepository.Interface;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class FeeService : IFeeService
{
    private readonly IFeeRepository feeRepository;

    public FeeService(IFeeRepository feeRepository)
    {
        this.feeRepository = feeRepository;
    }

    public double GetFeeSubscribe(string creatorEmail)
    {
        return this.feeRepository.GetFeeByCreatorEmail(creatorEmail);
    }
}