using PromoPilot.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoPilot.Application.UseCases.Budgets
{
    public interface ICreateBudgetUseCase
    {
        Task<BudgetDto> ExecuteAsync(BudgetDto dto);
    }

    public interface IGetAllBudgetsUseCase
    {
        Task<IEnumerable<BudgetDto>> ExecuteAsync();
    }

    public interface IGetBudgetByIdUseCase
    {
        Task<BudgetDto> ExecuteAsync(int id);
    }

    public interface IUpdateBudgetUseCase
    {
        Task<BudgetDto> ExecuteAsync(int id, BudgetDto dto);
    }

    public interface IDeleteBudgetUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }
}
