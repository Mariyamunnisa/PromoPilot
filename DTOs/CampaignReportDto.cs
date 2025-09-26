using System;
using System.ComponentModel.DataAnnotations;

public class CampaignReportDto
{
    // No need for [Key] if this is auto-generated in the DB
    public int ReportID { get; set; }

    [Required(ErrorMessage = "Campaign ID is required.")]
    public int CampaignID { get; set; }

    [Required(ErrorMessage = "ROI is required.")]
    [Range(0.0, double.MaxValue, ErrorMessage = "ROI must be a non-negative value.")]
    public double ROI { get; set; }

    [Required(ErrorMessage = "Reach is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Reach must be a non-negative number.")]
    public int Reach { get; set; }

    [Required(ErrorMessage = "Conversion rate is required.")]
    [Range(0.0, 100.0, ErrorMessage = "Conversion rate must be between 0 and 100.")]
    public double ConversionRate { get; set; }

    [Required(ErrorMessage = "Generated date is required.")]
    public DateTime GeneratedDate { get; set; }
}
