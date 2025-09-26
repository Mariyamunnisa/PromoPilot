using Microsoft.EntityFrameworkCore;
using PromoPilot.Core.Entities;
using PromoPilot.Core.Interfaces;
using PromoPilot.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Infrastructure.Repositories
{
    public class CampaignReportRepository : ICampaignReportRepository
    {
        private readonly PromoPilotDbContext _context;

        public CampaignReportRepository(PromoPilotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CampaignReport>> GetAllAsync()
        {
            return await _context.CampaignReports.ToListAsync();
        }

        public async Task<CampaignReport?> GetByIdAsync(int id)
        {
            return await _context.CampaignReports.FindAsync(id);
        }

        public async Task AddAsync(CampaignReport report)
        {
            await _context.CampaignReports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CampaignReport report)
        {
            _context.CampaignReports.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var report = await _context.CampaignReports.FindAsync(id);
            if (report != null)
            {
                _context.CampaignReports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}
