using System.ComponentModel.DataAnnotations;

namespace OtonomFinansAPI.Models;

public class InvoiceRecord
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string DocumentName { get; set; } = string.Empty;
    
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    
    [Required]
    public string RawText { get; set; } = string.Empty;
    
    public decimal TotalAmount { get; set; }
    
    public decimal TaxAmount { get; set; }
    
    public string? VendorName { get; set; }
    
    public bool IsProcessed { get; set; }
}