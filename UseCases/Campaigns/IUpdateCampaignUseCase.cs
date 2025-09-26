using PromoPilot.Application.DTOs;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public interface IUpdateCampaignUseCase
    {
        Task<CampaignDto> ExecuteAsync(int id, CampaignDto dto);
    }
}
