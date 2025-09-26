using PromoPilot.Application.DTOs;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public interface IGetCampaignByIdUseCase
    {
        Task<CampaignDto> ExecuteAsync(int id);
    }
}
