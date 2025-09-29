using Microsoft.AspNetCore.Mvc;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.UseCases.Budgets;
using AutoMapper;
using PromoPilot.Core.Entities;

namespace PromoPilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly ICreateBudgetUseCase _create;
        private readonly IGetAllBudgetsUseCase _getAll;
        private readonly IGetBudgetByIdUseCase _getById;
        private readonly IUpdateBudgetUseCase _update;
        private readonly IDeleteBudgetUseCase _delete;
        private readonly IMapper _mapper;

        public BudgetController(
            ICreateBudgetUseCase create,
            IGetAllBudgetsUseCase getAll,
            IGetBudgetByIdUseCase getById,
            IUpdateBudgetUseCase update,
            IDeleteBudgetUseCase delete,
            IMapper mapper)
        {
            _create = create;
            _getAll = getAll;
            _getById = getById;
            _update = update;
            _delete = delete;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _getAll.ExecuteAsync();
            var dtos = _mapper.Map<IEnumerable<BudgetDto>>(entities);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _getById.ExecuteAsync(id);
            if (entity == null)
            {
                return Problem(
                    type: "https://promopilot.com/errors/not-found",
                    title: "Budget Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    detail: $"No budget found with ID {id}",
                    instance: HttpContext.Request.Path
                );
            }

            var dto = _mapper.Map<BudgetDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BudgetDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var resultDto = await _create.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultDto.BudgetID }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BudgetDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var entity = await _update.ExecuteAsync(id, dto);
            if (entity == null)
            {
                return Problem(
                    type: "https://promopilot.com/errors/not-found",
                    title: "Budget Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    detail: $"No budget found with ID {id} to update.",
                    instance: HttpContext.Request.Path
                );
            }

            var resultDto = _mapper.Map<BudgetDto>(entity);
            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _delete.ExecuteAsync(id);
            if (!deleted)
            {
                return Problem(
                    type: "https://promopilot.com/errors/not-found",
                    title: "Budget Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    detail: $"No budget found with ID {id} to delete.",
                    instance: HttpContext.Request.Path
                );
            }

            return NoContent();
        }
    }
}
