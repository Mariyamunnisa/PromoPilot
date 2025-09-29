using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using PromoPilot.Application.UseCases.CampaignReports;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.CampaignReports
{
    public class GenerateCampaignReportUseCase : IGenerateCampaignReportUseCase
    {
        private readonly ICampaignReportService _reportService;

        public GenerateCampaignReportUseCase(ICampaignReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<CampaignReportDto?> ExecuteAsync(int campaignId)
        {
            return await _reportService.GenerateReportAsync(campaignId);
        }
    }
}
