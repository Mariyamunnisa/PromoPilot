using PromoPilot.Application.Interfaces;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public class DeleteCampaignUseCase : IDeleteCampaignUseCase
    {
        private readonly ICampaignService _service;

        public DeleteCampaignUseCase(ICampaignService service)
        {
            _service = service;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return false;

            await _service.DeleteAsync(id);
            return true;
        }
    }
}
