using System.ComponentModel.DataAnnotations;

public class ExecutionStatusDto
{
    // Assuming StatusID is auto-generated
    public int StatusID { get; set; }

    [Required(ErrorMessage = "Campaign ID is required.")]
    public int CampaignID { get; set; }

    [Required(ErrorMessage = "Store ID is required.")]
    public int StoreID { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [StringLength(50, ErrorMessage = "Status can't be longer than 50 characters.")]
    public string? Status { get; set; }

    [StringLength(500, ErrorMessage = "Feedback can't be longer than 500 characters.")]
    public string? Feedback { get; set; }
}
