using AutoMapper;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using PromoPilot.Core.Entities;

namespace PromoPilot.Application.UseCases.Budgets
{
    public class CreateBudgetUseCase : ICreateBudgetUseCase
    {
        private readonly IBudgetService _service;
        private readonly IMapper _mapper;

        public CreateBudgetUseCase(IBudgetService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<BudgetDto> ExecuteAsync(BudgetDto dto)
        {
            var entity = _mapper.Map<Budget>(dto);
            await _service.AllocateBudgetAsync(entity);
            return _mapper.Map<BudgetDto>(entity);
        }

    }

    public class GetAllBudgetsUseCase : IGetAllBudgetsUseCase
    {
        private readonly IBudgetService _service;
        private readonly IMapper _mapper;

        public GetAllBudgetsUseCase(IBudgetService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BudgetDto>> ExecuteAsync()
        {
            var entities = await _service.GetAllBudgetsAsync();
            return _mapper.Map<IEnumerable<BudgetDto>>(entities);
        }

    }

    public class GetBudgetByIdUseCase : IGetBudgetByIdUseCase
    {
        private readonly IBudgetService _service;
        private readonly IMapper _mapper;

        public GetBudgetByIdUseCase(IBudgetService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<BudgetDto> ExecuteAsync(int id)
        {
            var entity = await _service.GetBudgetDetailsAsync(id);
            return _mapper.Map<BudgetDto>(entity);
        }

    }

    public class UpdateBudgetUseCase : IUpdateBudgetUseCase
    {
        private readonly IBudgetService _service;
        private readonly IMapper _mapper;

        public UpdateBudgetUseCase(IBudgetService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<BudgetDto> ExecuteAsync(int id, BudgetDto dto)
        {
            var existing = await _service.GetBudgetDetailsAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            await _service.TrackBudgetSpendAsync(existing);
            return _mapper.Map<BudgetDto>(existing);
        }

    }

    public class DeleteBudgetUseCase : IDeleteBudgetUseCase
    {
        private readonly IBudgetService _service;

        public DeleteBudgetUseCase(IBudgetService service)
        {
            _service = service;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var existing = await _service.GetBudgetDetailsAsync(id);
            if (existing == null) return false;

            await _service.RemoveBudgetAllocationAsync(id);
            return true;
        }

    }
}
