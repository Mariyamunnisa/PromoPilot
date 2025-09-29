using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using PromoPilot.Application.UseCases.CampaignReports;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.CampaignReports.Implementations
{
    public class GetCampaignReportByIdUseCase : IGetCampaignReportByIdUseCase
    {
        private readonly ICampaignReportService _reportService;

        public GetCampaignReportByIdUseCase(ICampaignReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<CampaignReportDto?> ExecuteAsync(int id)
        {
            return await _reportService.GetReportByIdAsync(id);
        }
    }
}
