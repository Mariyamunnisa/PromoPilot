using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoPilot.Application.DTOs;
using PromoPilot.Core.Entities;

namespace PromoPilot.Application.UseCases.Engagements
{
    public interface ITrackEngagementUseCase
    {
        Task<Engagement> ExecuteAsync(EngagementDto dto);
    }

    public interface IGetAllEngagementsUseCase
    {
        Task<IEnumerable<Engagement>> ExecuteAsync();
    }

    public interface IGetEngagementByIdUseCase
    {
        Task<Engagement> ExecuteAsync(int id);
    }

    public interface IUpdateEngagementUseCase
    {
        Task<Engagement> ExecuteAsync(int id, EngagementDto dto);
    }

    public interface IDeleteEngagementUseCase
    {
        Task<bool> ExecuteAsync(int id);
    }

}
