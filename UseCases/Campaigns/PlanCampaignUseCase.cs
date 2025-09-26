using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public class PlanCampaignUseCase : IPlanCampaignUseCase
    {
        private readonly ICampaignService _service;

        public PlanCampaignUseCase(ICampaignService service)
        {
            _service = service;
        }

        public async Task<CampaignDto> ExecuteAsync(CampaignDto dto)
        {
            return await _service.AddAsync(dto);
        }
    }
}
