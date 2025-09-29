using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoPilot.Application.Interfaces;
using PromoPilot.Application.DTOs;

namespace PromoPilot.Application.UseCases.Engagements
{
    public class GetCustomerEngagementStatsUseCase : IGetCustomerEngagementStatsUseCase
    {
        private readonly IEngagementService _service;

        public GetCustomerEngagementStatsUseCase(IEngagementService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<CustomerEngagementStatsDto>> ExecuteAsync()
        {
            var engagements = await _service.GetAllAsync();

            var stats = engagements
                .GroupBy(e => new { e.CustomerId, e.CampaignId })
                .Select(g => new CustomerEngagementStatsDto
                {
                    CustomerID = g.Key.CustomerId,
                    CampaignID = g.Key.CampaignId,
                    TotalRedemptions = g.Sum(e => e.RedemptionCount),
                    TotalPurchaseValue = g.Sum(e => e.PurchaseValue),
                    VisitCount = g.Count()
                });

            return stats;
        }
    }

}
