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

        // 🟢 Plan a new campaign
        [HttpPost("plan")]
        public async Task<IActionResult> PlanCampaign([FromBody] CampaignDto dto)
        {
            var result = await _useCase.PlanAsync(dto);
            return CreatedAtAction(nameof(GetCampaignById), new { id = result.CampaignID }, result);
        }

        // 🟢 Get all planned campaigns
        [HttpGet]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var result = await _useCase.GetAllAsync();
            return Ok(result);
        }

        // 🟢 Get campaign by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            var result = await _useCase.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // 🟢 Update campaign details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampaign(int id, [FromBody] CampaignDto dto)
        {
            var result = await _useCase.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // 🔴 Cancel a campaign
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelCampaign(int id)
        {
            var success = await _useCase.CancelAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // 🟢 Schedule campaign to stores
        [HttpPut("schedule/{id}")]
        public async Task<IActionResult> ScheduleCampaign(int id, [FromBody] string storeList)
        {
            if (string.IsNullOrWhiteSpace(storeList))
                return BadRequest("Store list cannot be empty.");

            try
            {
                var result = await _useCase.ScheduleAsync(id, storeList);
                if (result == null)
                    return NotFound($"Campaign with ID {id} not found.");

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // For validation errors
            }
            catch (Exception ex)
            {
                // Log the error if needed
                return StatusCode(500, "An unexpected error occurred.");
            }
        }


    }
}
