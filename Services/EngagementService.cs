using PromoPilot.Application.Interfaces;
using PromoPilot.Core.Entities;
using PromoPilot.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace PromoPilot.Application.Services
{
    public class EngagementService : IEngagementService
    {
        private readonly IEngagementRepository _repository;
        private readonly ILogger<EngagementService> _logger;

        public EngagementService(IEngagementRepository repository, ILogger<EngagementService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Engagement>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all engagements.");
            return await _repository.GetAllAsync();
        }

        public async Task<Engagement> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching engagement with ID: {Id}", id);
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Engagement engagement)
        {
            _logger.LogInformation("Adding new engagement.");
            await _repository.AddAsync(engagement);
        }

        public async Task UpdateAsync(Engagement engagement)
        {
            _logger.LogInformation("Updating engagement with ID: {Id}", engagement.EngagementId);
            await _repository.UpdateAsync(engagement);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting engagement with ID: {Id}", id);
            await _repository.DeleteAsync(id);
        }
    }
}
