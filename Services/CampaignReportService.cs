using AutoMapper;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;
using PromoPilot.Core.Interfaces;
using PromoPilot.Application.Interfaces;

public class CampaignReportService 
{
    private readonly ICampaignReportRepository _repository;
    private readonly IMapper _mapper;

    public CampaignReportService(ICampaignReportRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CampaignReportDto>> GetAllAsync()
    {
        var campaignsReport = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CampaignReportDto>>(campaignsReport);
    }

    public async Task<CampaignReportDto> GetByIdAsync(int id)
    {
        var campaignReport = await _repository.GetByIdAsync(id);
        return _mapper.Map<CampaignReportDto>(campaignReport);
    }

    public async Task<CampaignReportDto> AddAsync(CampaignReportDto dto)
    {
        var campaignReport = _mapper.Map<CampaignReport>(dto);
        await _repository.AddAsync(campaignReport);
        return _mapper.Map<CampaignReportDto>(campaignReport);
    }

    public async Task<CampaignReportDto> UpdateAsync(CampaignReportDto dto)
    {
        var campaignReport = _mapper.Map<CampaignReport>(dto);
        await _repository.UpdateAsync(campaignReport);
        return _mapper.Map<CampaignReportDto>(campaignReport);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
