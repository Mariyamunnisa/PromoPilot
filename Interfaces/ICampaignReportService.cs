using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Application.Interfaces
{
    public interface ICampaignReportService
    {
        Task<IEnumerable<CampaignReport>> GetAllAsync();
        Task<IEnumerable<CampaignReportDto>> GetAllReportsAsync();
        Task<CampaignReportDto?> GenerateReportAsync(int campaignId);
        Task<CampaignReportDto?> GetReportByIdAsync(int id);
        Task<CampaignReportDto> GetByIdAsync(int id);
        Task<CampaignReportDto> AddAsync(CampaignReport report);
        Task<CampaignReportDto> UpdateAsync(CampaignReport report);
        Task<CampaignReportDto> DeleteAsync(int id);
    }

}
