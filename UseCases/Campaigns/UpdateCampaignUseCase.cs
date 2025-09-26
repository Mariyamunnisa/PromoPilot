using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public class UpdateCampaignUseCase : IUpdateCampaignUseCase
    {
        private readonly ICampaignService _service;

        public UpdateCampaignUseCase(ICampaignService service)
        {
            _service = service;
        }

        public async Task<CampaignDto> ExecuteAsync(int id, CampaignDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return null;

            dto.CampaignID = id;
            return await _service.UpdateAsync(dto);
        }
    }
}
