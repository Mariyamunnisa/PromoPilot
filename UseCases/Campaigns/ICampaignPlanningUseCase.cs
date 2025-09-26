using PromoPilot.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public interface ICampaignPlanningUseCase
    {
        Task<IEnumerable<CampaignDto>> GetAllAsync();
        Task<CampaignDto> GetByIdAsync(int id);
        Task<CampaignDto> PlanAsync(CampaignDto dto);
        Task<CampaignDto> UpdateAsync(int id, CampaignDto dto);
        Task<bool> CancelAsync(int id);
        Task<CampaignDto> ScheduleAsync(int campaignId, string storeList);

    }
}
