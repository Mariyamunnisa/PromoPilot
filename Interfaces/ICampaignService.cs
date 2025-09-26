using PromoPilot.Application.DTOs;

public interface ICampaignService
{
    Task<IEnumerable<CampaignDto>> GetAllAsync();
    Task<CampaignDto> GetByIdAsync(int id);
    Task<CampaignDto> AddAsync(CampaignDto dto);
    Task<CampaignDto> UpdateAsync(CampaignDto dto);
    Task DeleteAsync(int id);
}
