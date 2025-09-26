using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public class GetAllCampaignsUseCase : IGetAllCampaignsUseCase
    {
        private readonly ICampaignService _service;

        public GetAllCampaignsUseCase(ICampaignService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<CampaignDto>> ExecuteAsync()
        {
            return await _service.GetAllAsync();
        }
    }
}
