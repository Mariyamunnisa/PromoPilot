using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;
using PromoPilot.Application.Interfaces;

namespace PromoPilot.Application.UseCases.Products
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IProductService _repo;
        private readonly IMapper _mapper;

        public CreateProductUseCase(IProductService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Product> ExecuteAsync(ProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _repo.AddAsync(entity);
            return entity;
        }
    }

    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductService _repo;

        public GetAllProductsUseCase(IProductService repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Product>> ExecuteAsync() => await _repo.GetAllAsync();
    }

    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {
        private readonly IProductService _repo;

        public GetProductByIdUseCase(IProductService repo)
        {
            _repo = repo;
        }

        public async Task<Product> ExecuteAsync(int id) => await _repo.GetByIdAsync(id);
    }

    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IProductService _repo;
        private readonly IMapper _mapper;

        public UpdateProductUseCase(IProductService repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Product> ExecuteAsync(int id, ProductDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return entity;
        }
    }

    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductService _repo;

        public DeleteProductUseCase(IProductService repo)
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
