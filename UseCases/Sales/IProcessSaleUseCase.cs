using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;

namespace PromoPilot.Application.UseCases.Sales
{
    public interface IProcessSaleUseCase
    {
        Task<Sale> ExecuteAsync(SaleDto dto);
    }

    public interface IGetAllSalesUseCase
    {
        Task<IEnumerable<Sale>> ExecuteAsync();
    }

    public interface IGetSaleByIdUseCase
    {
        Task<Sale> ExecuteAsync(int id);
    }

    public interface IUpdateSaleUseCase
    {
        Task<Sale> ExecuteAsync(int id, SaleDto dto);
    }

    public interface IDeleteSaleUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }

}
