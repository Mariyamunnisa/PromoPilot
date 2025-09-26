using PromoPilot.Application.DTOs;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public interface IPlanCampaignUseCase
    {
        Task<CampaignDto> ExecuteAsync(CampaignDto dto);
    }
}
