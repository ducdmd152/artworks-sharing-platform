using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using ArtHubRepository.Repository;
using ArtHubService.Interface;

namespace ArtHubService.Service;

public class FeeService : IFeeService
{
    private readonly IFeeRepository feeRepository;
    private readonly IUnitOfWork unitOfWork;

    public FeeService(IFeeRepository feeRepository, IUnitOfWork unitOfWork)
    {
        this.feeRepository = feeRepository;
        this.unitOfWork = unitOfWork;
    }

    public Fee? GetFeeByArtistEmail(string email)
    {
        return feeRepository.GetFeeByArtistEmail(email);
    }

    public async Task<Fee?> UpdateAsync(Fee fee)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            var updatedFee = feeRepository.Update(fee);
            await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            return updatedFee;
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
        }
        return null;
    }
}
