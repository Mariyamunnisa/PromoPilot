using PromoPilot.Application.Interfaces;
using PromoPilot.Core.Entities;
using PromoPilot.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace PromoPilot.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all products.");
            return await _repository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching product with ID: {Id}", id);
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            _logger.LogInformation("Adding new product.");
            await _repository.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _logger.LogInformation("Updating product with ID: {Id}", product.ProductId);
            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);
            await _repository.DeleteAsync(id);
        }
    }
}
