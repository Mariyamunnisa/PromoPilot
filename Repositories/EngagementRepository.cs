using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoPilot.Core.Interfaces;
using PromoPilot.Core.Entities;
using PromoPilot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PromoPilot.Infrastructure.Repositories
{
    public class EngagementRepository : IEngagementRepository
    {
        private readonly PromoPilotDbContext _context;
        public EngagementRepository(PromoPilotDbContext context) => _context = context;

        public async Task<IEnumerable<Engagement>> GetAllAsync() => await _context.Engagements.ToListAsync();
        public async Task<Engagement> GetByIdAsync(int id) => await _context.Engagements.FindAsync(id);
        public async Task AddAsync(Engagement engagement)
        {
            _context.Engagements.Add(engagement);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Engagement engagement)
        {
            _context.Engagements.Update(engagement);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Engagements.FindAsync(id);
            if (entity != null)
            {
                _context.Engagements.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
