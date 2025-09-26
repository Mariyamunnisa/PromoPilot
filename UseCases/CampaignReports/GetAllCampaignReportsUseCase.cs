using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using PromoPilot.Application.UseCases.CampaignReports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.CampaignReports.Implementations
{
    public class GetAllCampaignReportsUseCase : IGetAllCampaignReportsUseCase
    {
        private readonly ICampaignReportService _reportService;

        public GetAllCampaignReportsUseCase(ICampaignReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IEnumerable<CampaignReportDto>> ExecuteAsync()
        {
            return await _reportService.GetAllReportsAsync();
        }
    }
}
