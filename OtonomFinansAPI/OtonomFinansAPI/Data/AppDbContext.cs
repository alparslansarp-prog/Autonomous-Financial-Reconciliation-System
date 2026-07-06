using Microsoft.EntityFrameworkCore;
using OtonomFinansAPI.Models;

namespace OtonomFinansAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<InvoiceRecord> Invoices { get; set; }
    public DbSet<CategorizationResult> CategorizationResults { get; set; }
}