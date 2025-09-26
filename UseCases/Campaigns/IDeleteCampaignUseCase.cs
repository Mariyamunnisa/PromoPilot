using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public interface IDeleteCampaignUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }
}
