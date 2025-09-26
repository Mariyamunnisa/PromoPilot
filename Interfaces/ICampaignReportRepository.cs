using PromoPilot.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Core.Interfaces
{
    public interface ICampaignReportRepository
    {
        Task<IEnumerable<CampaignReport>> GetAllAsync();
        Task<CampaignReport?> GetByIdAsync(int id);
        Task AddAsync(CampaignReport report);
        Task UpdateAsync(CampaignReport report);
        Task DeleteAsync(int id);
    }
}
