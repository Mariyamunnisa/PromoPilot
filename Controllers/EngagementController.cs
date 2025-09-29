using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.UseCases.Engagements;
using PromoPilot.Core.Entities;
using PromoPilot.Application.Interfaces;

namespace PromoPilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EngagementController : ControllerBase
    {
        private readonly ITrackEngagementUseCase _create;
        private readonly IGetAllEngagementsUseCase _getAll;
        private readonly IGetEngagementByIdUseCase _getById;
        private readonly IUpdateEngagementUseCase _update;
        private readonly IDeleteEngagementUseCase _delete;
        private readonly IMapper _mapper;
        private readonly ILogger<EngagementController> _logger;
        private readonly IGetCustomerEngagementStatsUseCase _stats;

        public EngagementController(
            ITrackEngagementUseCase create,
            IGetAllEngagementsUseCase getAll,
            IGetEngagementByIdUseCase getById,
            IUpdateEngagementUseCase update,
            IDeleteEngagementUseCase delete,
            IMapper mapper,
            IGetCustomerEngagementStatsUseCase stats,
            ILogger<EngagementController> logger)
        {
            _create = create;
            _getAll = getAll;
            _getById = getById;
            _update = update;
            _delete = delete;
            _mapper = mapper;
            _logger = logger;
            _stats = stats;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all engagements.");
            var entities = await _getAll.ExecuteAsync();
            var dtos = _mapper.Map<IEnumerable<EngagementDto>>(entities);
            return Ok(dtos);
        }
        [HttpGet("CustomerStats")]
        public async Task<IActionResult> GetCustomerStats()
        {
            _logger.LogInformation("Fetching customer engagement statistics.");
            var result = await _stats.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching engagement with ID: {Id}", id);
            var entity = await _getById.ExecuteAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Engagement with ID {Id} not found.", id);
                return NotFound();
            }
            var dto = _mapper.Map<EngagementDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EngagementDto dto)
        {
            _logger.LogInformation("Creating new engagement.");
            var entity = await _create.ExecuteAsync(dto);
            var resultDto = _mapper.Map<EngagementDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.EngagementID }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EngagementDto dto)
        {
            _logger.LogInformation("Updating engagement with ID: {Id}", id);
            var entity = await _update.ExecuteAsync(id, dto);
            if (entity == null)
            {
                _logger.LogWarning("Engagement with ID {Id} not found for update.", id);
                return NotFound();
            }
            var resultDto = _mapper.Map<EngagementDto>(entity);
            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting engagement with ID: {Id}", id);
            var deleted = await _delete.ExecuteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
