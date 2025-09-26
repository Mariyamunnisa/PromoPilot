using PromoPilot.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.CampaignReports
{
    public interface IGenerateCampaignReportUseCase
    {
        Task<CampaignReportDto?> ExecuteAsync(int campaignId);
    }

    public interface IGetAllCampaignReportsUseCase
    {
        Task<IEnumerable<CampaignReportDto>> ExecuteAsync();
    }

    public interface IGetCampaignReportByIdUseCase
    {
        Task<CampaignReportDto?> ExecuteAsync(int id);
    }

}
