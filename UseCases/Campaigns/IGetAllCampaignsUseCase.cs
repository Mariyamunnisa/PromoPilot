using PromoPilot.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public interface IGetAllCampaignsUseCase
    {
        Task<IEnumerable<CampaignDto>> ExecuteAsync();
    }
}
