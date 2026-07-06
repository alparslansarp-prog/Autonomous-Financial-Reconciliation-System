using Microsoft.AspNetCore.Mvc;
using OtonomFinansAPI.Data;
using OtonomFinansAPI.Models;
using OtonomFinansAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class InvoiceProcessingController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IFinancialAIService _aiService;

    public InvoiceProcessingController(AppDbContext context, IFinancialAIService aiService)
    {
        _context = context;
        _aiService = aiService;
    }

    [HttpPost("process")]
    public async Task<IActionResult> Process([FromBody] InvoiceRecord invoice)
    {
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        try
        {
            var result = await _aiService.ProcessInvoiceAsync(invoice);
            _context.CategorizationResults.Add(result);
            invoice.IsProcessed = true;
            await _context.SaveChangesAsync();
            
            return Ok(new { Message = "İşlem başarıyla tamamlandı", Data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}