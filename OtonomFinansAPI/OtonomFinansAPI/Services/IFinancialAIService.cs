using OtonomFinansAPI.Models;

namespace OtonomFinansAPI.Services;

public interface IFinancialAIService
{
    Task<CategorizationResult> ProcessInvoiceAsync(InvoiceRecord invoice);
}