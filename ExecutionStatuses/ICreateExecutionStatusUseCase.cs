using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;

namespace PromoPilot.Application.UseCases.ExecutionStatuses
{
    public interface ICreateExecutionStatusUseCase
    {
        Task<ExecutionStatus> ExecuteAsync(ExecutionStatusDto dto);
    }

    public interface IGetAllExecutionStatusesUseCase
    {
        Task<IEnumerable<ExecutionStatus>> ExecuteAsync();
    }

    public interface IGetExecutionStatusByIdUseCase
    {
        Task<ExecutionStatus> ExecuteAsync(int id);
    }
    public interface IUpdateExecutionStatusUseCase
    {
        Task<bool> ExecuteAsync(ExecutionStatusDto dto);
    }

    public interface IDeleteExecutionStatusUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }

}
