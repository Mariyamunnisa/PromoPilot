using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Campaigns
{
    public class CampaignPlanningUseCase : ICampaignPlanningUseCase
    {
        private readonly IPlanCampaignUseCase _plan;
        private readonly IGetAllCampaignsUseCase _getAll;
        private readonly IGetCampaignByIdUseCase _getById;
        private readonly IUpdateCampaignUseCase _update;
        private readonly IDeleteCampaignUseCase _delete;
        private readonly IScheduleCampaignUseCase _schedule;
        public CampaignPlanningUseCase(
            IPlanCampaignUseCase plan,
            IGetAllCampaignsUseCase getAll,
            IGetCampaignByIdUseCase getById,
            IUpdateCampaignUseCase update,
            IDeleteCampaignUseCase delete)
        {
            _plan = plan;
            _getAll = getAll;
            _getById = getById;
            _update = update;
            _delete = delete;
        }

        public CampaignPlanningUseCase(
            IPlanCampaignUseCase plan,
            IGetAllCampaignsUseCase getAll,
            IGetCampaignByIdUseCase getById,
            IUpdateCampaignUseCase update,
            IDeleteCampaignUseCase delete,
            IScheduleCampaignUseCase schedule)
        {
            _plan = plan;
            _getAll = getAll;
            _getById = getById;
            _update = update;
            _delete = delete;
            _schedule = schedule;
        }

        public Task<CampaignDto> ScheduleAsync(int campaignId, string storeList) =>
            _schedule.ExecuteAsync(campaignId, storeList);

        public Task<IEnumerable<CampaignDto>> GetAllAsync() => _getAll.ExecuteAsync();

        public Task<CampaignDto> GetByIdAsync(int id) => _getById.ExecuteAsync(id);

        public Task<CampaignDto> PlanAsync(CampaignDto dto) => _plan.ExecuteAsync(dto);

        public Task<CampaignDto> UpdateAsync(int id, CampaignDto dto) => _update.ExecuteAsync(id, dto);

        public Task<bool> CancelAsync(int id) => _delete.ExecuteAsync(id);
    }
}
