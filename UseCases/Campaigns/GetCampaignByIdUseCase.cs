using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public class GetCampaignByIdUseCase : IGetCampaignByIdUseCase
    {
        private readonly ICampaignService _service;

        public GetCampaignByIdUseCase(ICampaignService service)
        {
            _service = service;
        }

        public async Task<CampaignDto> ExecuteAsync(int id)
        {
            return await _service.GetByIdAsync(id);
        }
    }
}
