using Microsoft.AspNetCore.Mvc;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.UseCases.Campaigns;
using Microsoft.Extensions.Logging;

namespace PromoPilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignPlanningUseCase _useCase;
        private readonly ILogger<CampaignController> _logger;

        public CampaignController(ICampaignPlanningUseCase useCase, ILogger<CampaignController> logger)
        {
            _useCase = useCase;
            _logger = logger;
        }

        [HttpPost("plan")]
        public async Task<IActionResult> PlanCampaign([FromBody] CampaignDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _useCase.PlanAsync(dto);
            return CreatedAtAction(nameof(GetCampaignById), new { id = result.CampaignID }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var result = await _useCase.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            var result = await _useCase.GetByIdAsync(id);
            if (result == null)
            {
                return Problem(
                    type: "https://promopilot.com/errors/not-found",
                    title: "Campaign Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    detail: $"No campaign found with ID {id}",
                    instance: HttpContext.Request.Path
                );
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampaign(int id, [FromBody] CampaignDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _useCase.UpdateAsync(id, dto);
            if (result == null)
            {
                return Problem(
                    type: "https://promopilot.com/errors/not-found",
                    title: "Campaign Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    detail: $"No campaign found with ID {id} to update.",
                    instance: HttpContext.Request.Path
                );
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelCampaign(int id)
        {
            var success = await _useCase.CancelAsync(id);
            if (!success)
            {
                return Problem(
                    type: "https://promopilot.com/errors/not-found",
                    title: "Campaign Not Found",
                    statusCode: StatusCodes.Status404NotFound,
                    detail: $"No campaign found with ID {id} to cancel.",
                    instance: HttpContext.Request.Path
                );
            }

            return NoContent();
        }

        [HttpPut("schedule/{id}")]
        public async Task<IActionResult> ScheduleCampaign(int id, [FromBody] string storeList)
        {
            if (string.IsNullOrWhiteSpace(storeList))
            {
                return Problem(
                    type: "https://promopilot.com/errors/invalid-input",
                    title: "Invalid Store List",
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: "Store list cannot be empty.",
                    instance: HttpContext.Request.Path
                );
            }

            try
            {
                var result = await _useCase.ScheduleAsync(id, storeList);
                if (result == null)
                {
                    return Problem(
                        type: "https://promopilot.com/errors/not-found",
                        title: "Campaign Not Found",
                        statusCode: StatusCodes.Status404NotFound,
                        detail: $"No campaign found with ID {id} to schedule.",
                        instance: HttpContext.Request.Path
                    );
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Problem(
                    type: "https://promopilot.com/errors/validation-error",
                    title: "Validation Error",
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: ex.Message,
                    instance: HttpContext.Request.Path
                );
            }
        }
    }
}
