using System;
using System.Collections.Generic;

namespace PromoPilot.Core.Entities;

public partial class CampaignReport
{
    public int ReportId { get; set; }

    public int CampaignId { get; set; }

    public decimal Roi { get; set; }

    public int Reach { get; set; }

    public decimal ConversionRate { get; set; }

    public DateTime GeneratedDate { get; set; }

    public virtual Campaign Campaign { get; set; } = null!;
}
