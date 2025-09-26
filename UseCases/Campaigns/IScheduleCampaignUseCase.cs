using PromoPilot.Application.DTOs;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public interface IScheduleCampaignUseCase
    {
        Task<CampaignDto> ExecuteAsync(int campaignId, string storeList);
    }
}
