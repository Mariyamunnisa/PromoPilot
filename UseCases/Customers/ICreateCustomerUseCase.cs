using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;

namespace PromoPilot.Application.UseCases.Customers
{
    public interface ICreateCustomerUseCase
    {
        Task<Customer> ExecuteAsync(CustomerDto dto);
    }

    public interface IGetAllCustomersUseCase
    {
        Task<IEnumerable<Customer>> ExecuteAsync();
    }

    public interface IGetCustomerByIdUseCase
    {
        Task<Customer> ExecuteAsync(int id);
    }

    public interface IUpdateCustomerUseCase
    {
        Task<Customer> ExecuteAsync(int id, CustomerDto dto);
    }

    public interface IDeleteCustomerUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }


}
