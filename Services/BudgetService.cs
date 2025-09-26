using Microsoft.Extensions.Logging;
using PromoPilot.Application.Interfaces;
using PromoPilot.Core.Entities;
using PromoPilot.Core.Interfaces;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _repository;
    private readonly ILogger<BudgetService> _logger;

    public BudgetService(IBudgetRepository repository, ILogger<BudgetService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Budget>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all budgets.");
        return await _repository.GetAllAsync();
    }

    public async Task<Budget> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching budget with ID: {Id}", id);
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Budget budget)
    {
        _logger.LogInformation("Adding new budget.");
        await _repository.AddAsync(budget);
    }

    public async Task UpdateAsync(Budget budget)
    {
        _logger.LogInformation("Updating budget with ID: {Id}", budget.BudgetId);
        await _repository.UpdateAsync(budget);
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation("Deleting budget with ID: {Id}", id);
        await _repository.DeleteAsync(id);
    }
}
