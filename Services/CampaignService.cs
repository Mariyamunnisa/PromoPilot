using AutoMapper;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;
using PromoPilot.Core.Interfaces;
public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _repository;
    private readonly IMapper _mapper;

    public CampaignService(ICampaignRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CampaignDto>> GetAllAsync()
    {
        var campaigns = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CampaignDto>>(campaigns);
    }

    public async Task<CampaignDto> GetByIdAsync(int id)
    {
        var campaign = await _repository.GetByIdAsync(id);
        return _mapper.Map<CampaignDto>(campaign);
    }

    public async Task<CampaignDto> AddAsync(CampaignDto dto)
    {
        var existing = await _repository.GetByNameAndDatesAsync(dto.Name, dto.StartDate, dto.EndDate);
        if (existing != null)
        {
            // Optionally return existing or throw a conflict
            throw new InvalidOperationException("Campaign already exists with same name and dates.");
        }

        var campaign = _mapper.Map<Campaign>(dto);
        await _repository.AddAsync(campaign);
        return _mapper.Map<CampaignDto>(campaign);
    }



    public async Task<CampaignDto> UpdateAsync(CampaignDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.CampaignID);
        if (existing == null) return null;

        _mapper.Map(dto, existing); // Update tracked entity
        await _repository.UpdateAsync(existing);
        return _mapper.Map<CampaignDto>(existing);
    }


    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
