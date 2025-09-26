using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using PromoPilot.Core.Entities;

namespace PromoPilot.Application.UseCases.ExecutionStatuses
{
    public class CreateExecutionStatusUseCase : ICreateExecutionStatusUseCase
    {
        private readonly IExecutionStatusService _repo;
        private readonly IMapper _mapper;

        public CreateExecutionStatusUseCase(IExecutionStatusService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ExecutionStatus> ExecuteAsync(ExecutionStatusDto dto)
        {
            var entity = _mapper.Map<ExecutionStatus>(dto);
            await _repo.AddAsync(entity);
            return entity;
        }
    }

    public class GetAllExecutionStatusesUseCase : IGetAllExecutionStatusesUseCase
    {
        private readonly IExecutionStatusService _repo;

        public GetAllExecutionStatusesUseCase(IExecutionStatusService repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ExecutionStatus>> ExecuteAsync() => await _repo.GetAllAsync();
    }

    public class GetExecutionStatusByIdUseCase : IGetExecutionStatusByIdUseCase
    {
        private readonly IExecutionStatusService _repo;

        public GetExecutionStatusByIdUseCase(IExecutionStatusService repo)
        {
            _repo = repo;
        }

        public async Task<ExecutionStatus> ExecuteAsync(int id) => await _repo.GetByIdAsync(id);
    }
    public class UpdateExecutionStatusUseCase : IUpdateExecutionStatusUseCase
    {
        private readonly IExecutionStatusService _service;
        private readonly IMapper _mapper;

        public UpdateExecutionStatusUseCase(IExecutionStatusService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(ExecutionStatusDto dto)
        {
            var existing = await _service.GetByIdAsync(dto.StatusID);
            if (existing == null) return false;

            existing.Status = dto.Status!;
            existing.Feedback = dto.Feedback ?? string.Empty;

            await _service.UpdateAsync(existing);
            return true;
        }
    }


    public class DeleteExecutionStatusUseCase : IDeleteExecutionStatusUseCase
    {
        private readonly IExecutionStatusService _repo;

        public DeleteExecutionStatusUseCase(IExecutionStatusService repo)
        {
            _repo = repo;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }

}
