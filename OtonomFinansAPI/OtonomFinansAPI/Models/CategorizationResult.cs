using System.ComponentModel.DataAnnotations;

namespace OtonomFinansAPI.Models;

public class CategorizationResult
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int InvoiceId { get; set; }
    
    [Required]
    public ExpenseCategory SuggestedCategory { get; set; }
    
    [Range(0, 100)]
    public int ConfidenceScore { get; set; }
    
    public string? Reasoning { get; set; }
    
    public bool NeedsManualReview { get; set; }
}