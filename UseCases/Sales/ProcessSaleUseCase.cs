using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;
using PromoPilot.Application.Interfaces;

namespace PromoPilot.Application.UseCases.Sales
{
    public class ProcessSaleUseCase : IProcessSaleUseCase
    {
        private readonly ISaleService _repo;
        private readonly IMapper _mapper;

        public ProcessSaleUseCase(ISaleService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Sale> ExecuteAsync(SaleDto dto)
        {
            var entity = _mapper.Map<Sale>(dto);
            await _repo.AddAsync(entity);
            return entity;
        }
    }

    public class GetAllSalesUseCase : IGetAllSalesUseCase
    {
        private readonly ISaleService _repo;

        public GetAllSalesUseCase(ISaleService repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Sale>> ExecuteAsync() => await _repo.GetAllAsync();
    }

    public class GetSaleByIdUseCase : IGetSaleByIdUseCase
    {
        private readonly ISaleService _repo;

        public GetSaleByIdUseCase(ISaleService repo)
        {
            _repo = repo;
        }

        public async Task<Sale> ExecuteAsync(int id) => await _repo.GetByIdAsync(id);
    }

    public class UpdateSaleUseCase : IUpdateSaleUseCase
    {
        private readonly ISaleService _repo;
        private readonly IMapper _mapper;

        public UpdateSaleUseCase(ISaleService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Sale> ExecuteAsync(int id, SaleDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return entity;
        }
    }

    public class DeleteSaleUseCase : IDeleteSaleUseCase
    {
        private readonly ISaleService _repo;

        public DeleteSaleUseCase(ISaleService repo)
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
