using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.CampaignReports.Implementations
{
    public class GetCampaignPerformanceByRegionUseCase : IGetCampaignPerformanceByRegionUseCase
    {
        private readonly ICampaignReportService _service;

        public GetCampaignPerformanceByRegionUseCase(ICampaignReportService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<CampaignRegionPerformanceDto>> ExecuteAsync()
        {
            var reports = await _service.GetAllReportsWithRegionAsync();

            var grouped = reports
                .GroupBy(r => new { r.Region, r.CampaignID })
                .Select(g => new CampaignRegionPerformanceDto
                {
                    Region = g.Key.Region,
                    CampaignID = g.Key.CampaignID,
                    TotalROI = g.Sum(x => x.ROI),
                    TotalReach = g.Sum(x => x.Reach),
                    AverageConversionRate = g.Average(x => x.ConversionRate)
                });

            return grouped;
        }
    }
}
