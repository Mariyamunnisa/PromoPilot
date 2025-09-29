using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.UseCases.ExecutionStatuses;
using PromoPilot.Core.Entities;

namespace PromoPilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExecutionStatusController : ControllerBase
    {
        private readonly ICreateExecutionStatusUseCase _create;
        private readonly IGetAllExecutionStatusesUseCase _getAll;
        private readonly IGetExecutionStatusByIdUseCase _getById;
        private readonly IUpdateExecutionStatusUseCase _update;
        private readonly IDeleteExecutionStatusUseCase _delete;
        private readonly IMapper _mapper;
        private readonly ILogger<ExecutionStatusController> _logger;

        public ExecutionStatusController(
            ICreateExecutionStatusUseCase create,
            IGetAllExecutionStatusesUseCase getAll,
            IGetExecutionStatusByIdUseCase getById,
            IUpdateExecutionStatusUseCase update,
            IDeleteExecutionStatusUseCase delete,
            IMapper mapper,
            ILogger<ExecutionStatusController> logger)
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
            _logger.LogInformation("Fetching all execution statuses.");
            var entities = await _getAll.ExecuteAsync();
            var dtos = _mapper.Map<IEnumerable<ExecutionStatusDto>>(entities);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching execution status with ID: {Id}", id);
            var entity = await _getById.ExecuteAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Execution status with ID {Id} not found.", id);
                return Problem(
                    type: "https://promopilot.com/errors/not-found",
                    title: "Execution Status Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    detail: $"No execution status found with ID {id}.",
                    instance: HttpContext.Request.Path
                );
            }

            var dto = _mapper.Map<ExecutionStatusDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ExecutionStatusDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogInformation("Creating new execution status.");
            var entity = await _create.ExecuteAsync(dto);
            var resultDto = _mapper.Map<ExecutionStatusDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.StatusID }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExecutionStatusDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            if (id != dto.StatusID)
            {
                return Problem(
                    type: "https://promopilot.com/errors/id-mismatch",
                    title: "ID Mismatch",
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: "The ID in the URL does not match the ID in the request body.",
                    instance: HttpContext.Request.Path
                );
            }

            _logger.LogInformation("Updating execution status with ID: {Id}", id);
            var updated = await _update.ExecuteAsync(dto);
            if (!updated)
            {
                return Problem(
                    type: "https://promopilot.com/errors/not-found",
                    title: "Execution Status Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    detail: $"No execution status found with ID {id} to update.",
                    instance: HttpContext.Request.Path
                );
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting execution status with ID: {Id}", id);
            var deleted = await _delete.ExecuteAsync(id);
            if (!deleted)
            {
                return Problem(
                    type: "https://promopilot.com/errors/not-found",
                    title: "Execution Status Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    detail: $"No execution status found with ID {id} to delete.",
                    instance: HttpContext.Request.Path
                );
            }

            return NoContent();
        }
    }
}
