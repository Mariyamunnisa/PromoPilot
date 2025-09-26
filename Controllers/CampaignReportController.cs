using Microsoft.AspNetCore.Mvc;
using PromoPilot.Application.UseCases.CampaignReports;

[ApiController]
[Route("api/[controller]")]
public class CampaignReportController : ControllerBase
{
    private readonly IGenerateCampaignReportUseCase _generate;
    private readonly IGetAllCampaignReportsUseCase _getAll;
    private readonly IGetCampaignReportByIdUseCase _getById;

    public CampaignReportController(
        IGenerateCampaignReportUseCase generate,
        IGetAllCampaignReportsUseCase getAll,
        IGetCampaignReportByIdUseCase getById)
    {
        _generate = generate;
        _getAll = getAll;
        _getById = getById;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _getAll.ExecuteAsync();
        return Ok(reports);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var report = await _getById.ExecuteAsync(id);
        if (report == null) return NotFound();
        return Ok(report);
    }

    [HttpPost("generate/{campaignId}")]
    public async Task<IActionResult> Generate(int campaignId)
    {
        var report = await _generate.ExecuteAsync(campaignId);
        if (report == null) return NotFound($"No report generated for Campaign ID {campaignId}");
        return Ok(report);
    }
}
