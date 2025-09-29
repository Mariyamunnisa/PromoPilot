using Microsoft.AspNetCore.Mvc;
using PromoPilot.Application.Interfaces;

namespace PromoPilot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignReportController : ControllerBase
    {
        private readonly ICampaignReportService _reportService;

        public CampaignReportController(ICampaignReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("CompareByRegion")]
        public async Task<IActionResult> CompareByRegion()
        {
            var result = await _reportService.CompareByRegionAsync();
            return Ok(result);
        }

        [HttpPost("generate/{id}")]
        public async Task<IActionResult> GenerateReport(int id)
        {
            try
            {
                var report = await _reportService.GenerateReportAsync(id);
                return Ok(report);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { StatusCode = 404, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "Unexpected error", Details = ex.Message });
            }
        }
    }
}
