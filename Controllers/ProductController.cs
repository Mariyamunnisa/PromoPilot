using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.UseCases.Products;
using PromoPilot.Core.Entities;
using PromoPilot.Application.Interfaces;

namespace PromoPilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICreateProductUseCase _create;
        private readonly IGetAllProductsUseCase _getAll;
        private readonly IGetProductByIdUseCase _getById;
        private readonly IUpdateProductUseCase _update;
        private readonly IDeleteProductUseCase _delete;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            ICreateProductUseCase create,
            IGetAllProductsUseCase getAll,
            IGetProductByIdUseCase getById,
            IUpdateProductUseCase update,
            IDeleteProductUseCase delete,
            IMapper mapper,
            ILogger<ProductController> logger)
        {
            _create = create;
            _getAll = getAll;
            _getById = getById;
            _update = update;
            _delete = delete;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all products.");
            var entities = await _getAll.ExecuteAsync();
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(entities);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching product with ID: {Id}", id);
            var entity = await _getById.ExecuteAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Product with ID {Id} not found.", id);
                return NotFound();
            }
            var dto = _mapper.Map<ProductDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto dto)
        {
            _logger.LogInformation("Creating new product.");
            var entity = await _create.ExecuteAsync(dto);
            var resultDto = _mapper.Map<ProductDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.ProductID }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            _logger.LogInformation("Updating product with ID: {Id}", id);
            var entity = await _update.ExecuteAsync(id, dto);
            if (entity == null)
            {
                _logger.LogWarning("Product with ID {Id} not found for update.", id);
                return NotFound();
            }
            var resultDto = _mapper.Map<ProductDto>(entity);
            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);
            var deleted = await _delete.ExecuteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
