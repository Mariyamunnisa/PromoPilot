using PromoPilot.Application.Interfaces;
using PromoPilot.Core.Entities;
using PromoPilot.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace PromoPilot.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all customers.");
            return await _repository.GetAllAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching customer with ID: {Id}", id);
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Customer customer)
        {
            _logger.LogInformation("Adding new customer.");
            await _repository.AddAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _logger.LogInformation("Updating customer with ID: {Id}", customer.CustomerId);
            await _repository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting customer with ID: {Id}", id);
            await _repository.DeleteAsync(id);
        }
    }
}
