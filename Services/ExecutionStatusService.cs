using PromoPilot.Application.Interfaces;
using PromoPilot.Core.Entities;
using PromoPilot.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace PromoPilot.Application.Services
{
    public class ExecutionStatusService : IExecutionStatusService
    {
        private readonly IExecutionStatusRepository _repository;
        private readonly ILogger<ExecutionStatusService> _logger;

        public ExecutionStatusService(IExecutionStatusRepository repository, ILogger<ExecutionStatusService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<ExecutionStatus>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all execution statuses.");
            return await _repository.GetAllAsync();
        }

        public async Task<ExecutionStatus> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching execution status with ID: {Id}", id);
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(ExecutionStatus status)
        {
            _logger.LogInformation("Adding new execution status.");
            await _repository.AddAsync(status);
        }

        public async Task UpdateAsync(ExecutionStatus status)
        {
            _logger.LogInformation("Updating execution status with ID: {Id}", status.StatusId);
            await _repository.UpdateAsync(status);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting execution status with ID: {Id}", id);
            await _repository.DeleteAsync(id);
        }
    }
}
