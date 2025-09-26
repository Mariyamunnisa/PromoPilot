using PromoPilot.Application.Interfaces;
using PromoPilot.Core.Entities;
using PromoPilot.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace PromoPilot.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository repository, ILogger<SaleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all sales.");
            return await _repository.GetAllAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching sale with ID: {Id}", id);
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Sale sale)
        {
            _logger.LogInformation("Adding new sale.");
            await _repository.AddAsync(sale);
        }

        public async Task UpdateAsync(Sale sale)
        {
            _logger.LogInformation("Updating sale with ID: {Id}", sale.SaleId);
            await _repository.UpdateAsync(sale);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting sale with ID: {Id}", id);
            await _repository.DeleteAsync(id);
        }
    }
}
