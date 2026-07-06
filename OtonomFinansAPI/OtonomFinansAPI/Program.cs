using Microsoft.EntityFrameworkCore;
using OtonomFinansAPI.Data;
using OtonomFinansAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://127.0.0.1:5004");

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("FinancialDb"));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinancialAIService, FinancialAIService>();

// CORS Politikası (Her yerden gelen isteğe izin ver)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// SORUN ÇIKARAN HTTPS YÖNLENDİRMESİNİ YEREL ORTAM İÇİN DEVRE DIŞI BIRAKTIK
// app.UseHttpsRedirection(); 

// CORS'u MapControllers'dan hemen önce devreye aldık
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();
app.Run();