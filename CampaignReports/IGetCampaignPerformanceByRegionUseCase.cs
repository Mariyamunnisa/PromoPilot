using PromoPilot.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.CampaignReports
{
    public interface IGetCampaignPerformanceByRegionUseCase
    {
        Task<IEnumerable<CampaignRegionPerformanceDto>> ExecuteAsync();
    }
}
