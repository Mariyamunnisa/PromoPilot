using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public class ScheduleCampaignUseCase : IScheduleCampaignUseCase
    {
        private readonly ICampaignService _service;

        public ScheduleCampaignUseCase(ICampaignService service)
        {
            _service = service;
        }

        public async Task<CampaignDto> ExecuteAsync(int campaignId, string storeList)
        {
            var campaign = await _service.GetByIdAsync(campaignId);
            if (campaign == null) return null;

            campaign.StoreList = storeList;
            return await _service.UpdateAsync(campaign);
        }

    }
}
