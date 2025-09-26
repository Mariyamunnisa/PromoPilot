using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;

namespace PromoPilot.Application.UseCases.Products
{
    public interface ICreateProductUseCase
    {
        Task<Product> ExecuteAsync(ProductDto dto);
    }

    public interface IGetAllProductsUseCase
    {
        Task<IEnumerable<Product>> ExecuteAsync();
    }

    public interface IGetProductByIdUseCase
    {
        Task<Product> ExecuteAsync(int id);
    }

    public interface IUpdateProductUseCase
    {
        Task<Product> ExecuteAsync(int id, ProductDto dto);
    }

    public interface IDeleteProductUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }

}
