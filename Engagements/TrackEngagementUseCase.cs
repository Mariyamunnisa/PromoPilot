using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PromoPilot.Application.DTOs;
using PromoPilot.Application.Interfaces;
using PromoPilot.Application.Services;
using PromoPilot.Core.Entities;

namespace PromoPilot.Application.UseCases.Engagements
{
    public class TrackEngagementUseCase : ITrackEngagementUseCase
    {
        private readonly IEngagementService _repo;
        private readonly IMapper _mapper;

        public TrackEngagementUseCase(IEngagementService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Engagement> ExecuteAsync(EngagementDto dto)
        {
            var entity = _mapper.Map<Engagement>(dto);
            await _repo.AddAsync(entity);
            return entity;
        }
    }

    public class GetAllEngagementsUseCase : IGetAllEngagementsUseCase
    {
        private readonly IEngagementService _repo;

        public GetAllEngagementsUseCase(IEngagementService repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Engagement>> ExecuteAsync() => await _repo.GetAllAsync();
    }

    public class GetEngagementByIdUseCase : IGetEngagementByIdUseCase
    {
        private readonly IEngagementService _repo;

        public GetEngagementByIdUseCase(IEngagementService repo)
        {
            _repo = repo;
        }

        public async Task<Engagement> ExecuteAsync(int id) => await _repo.GetByIdAsync(id);
    }

    public class UpdateEngagementUseCase : IUpdateEngagementUseCase
    {
        private readonly IEngagementService _repo;
        private readonly IMapper _mapper;

        public UpdateEngagementUseCase(IEngagementService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Engagement> ExecuteAsync(int id, EngagementDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return entity;
        }
    }

    public class DeleteEngagementUseCase : IDeleteEngagementUseCase
    {
        private readonly IEngagementService _repo;

        public DeleteEngagementUseCase(IEngagementService repo)
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
