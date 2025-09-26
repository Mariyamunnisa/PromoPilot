using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.UseCases.Sales;
using PromoPilot.Core.Entities;
using PromoPilot.Application.Interfaces;

namespace PromoPilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IProcessSaleUseCase _create;
        private readonly IGetAllSalesUseCase _getAll;
        private readonly IGetSaleByIdUseCase _getById;
        private readonly IUpdateSaleUseCase _update;
        private readonly IDeleteSaleUseCase _delete;
        private readonly IMapper _mapper;
        private readonly ILogger<SalesController> _logger;

        public SalesController(
            IProcessSaleUseCase create,
            IGetAllSalesUseCase getAll,
            IGetSaleByIdUseCase getById,
            IUpdateSaleUseCase update,
            IDeleteSaleUseCase delete,
            IMapper mapper,
            ILogger<SalesController> logger)
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
            _logger.LogInformation("Fetching all sales.");
            var entities = await _getAll.ExecuteAsync();
            var dtos = _mapper.Map<IEnumerable<SaleDto>>(entities);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching sale with ID: {Id}", id);
            var entity = await _getById.ExecuteAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Sale with ID {Id} not found.", id);
                return NotFound();
            }
            var dto = _mapper.Map<SaleDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SaleDto dto)
        {
            _logger.LogInformation("Processing new sale.");
            var entity = await _create.ExecuteAsync(dto);
            var resultDto = _mapper.Map<SaleDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.SaleId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaleDto dto)
        {
            _logger.LogInformation("Updating sale with ID: {Id}", id);
            var entity = await _update.ExecuteAsync(id, dto);
            if (entity == null)
            {
                _logger.LogWarning("Sale with ID {Id} not found for update.", id);
                return NotFound();
            }
            var resultDto = _mapper.Map<SaleDto>(entity);
            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting sale with ID: {Id}", id);
            var deleted = await _delete.ExecuteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
