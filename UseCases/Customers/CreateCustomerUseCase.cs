using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;
using PromoPilot.Application.Interfaces;

namespace PromoPilot.Application.UseCases.Customers
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerService _repo;
        private readonly IMapper _mapper;

        public CreateCustomerUseCase(ICustomerService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Customer> ExecuteAsync(CustomerDto dto)
        {
            var entity = _mapper.Map<Customer>(dto);
            await _repo.AddAsync(entity);
            return entity;
        }
    }

    public class GetAllCustomersUseCase : IGetAllCustomersUseCase
    {
        private readonly ICustomerService _repo;

        public GetAllCustomersUseCase(ICustomerService repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Customer>> ExecuteAsync() => await _repo.GetAllAsync();
    }

    public class GetCustomerByIdUseCase : IGetCustomerByIdUseCase
    {
        private readonly ICustomerService _repo;

        public GetCustomerByIdUseCase(ICustomerService repo)
        {
            _repo = repo;
        }

        public async Task<Customer> ExecuteAsync(int id) => await _repo.GetByIdAsync(id);
    }

    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly ICustomerService _repo;
        private readonly IMapper _mapper;

        public UpdateCustomerUseCase(ICustomerService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Customer> ExecuteAsync(int id, CustomerDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return entity;
        }
    }

    public class DeleteCustomerUseCase : IDeleteCustomerUseCase
    {
        private readonly ICustomerService _repo;

        public DeleteCustomerUseCase(ICustomerService repo)
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
