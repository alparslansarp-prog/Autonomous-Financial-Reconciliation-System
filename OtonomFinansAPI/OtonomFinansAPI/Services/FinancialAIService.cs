using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OtonomFinansAPI.Models;

namespace OtonomFinansAPI.Services;

public class FinancialAIService : IFinancialAIService
{
    private readonly ILogger<FinancialAIService> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public FinancialAIService(ILogger<FinancialAIService> logger, HttpClient httpClient, IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = httpClient;
        // appsettings.json dosyasındaki API anahtarını buraya otomatik çeker
        _apiKey = configuration["GeminiApiKey"] ?? throw new ArgumentNullException("GeminiApiKey eksik!");
    }

    public async Task<CategorizationResult> ProcessInvoiceAsync(InvoiceRecord invoice)
    {
        // 1. Guardrail Katmanı (Domain Kuralı)
        if (invoice.TaxAmount > invoice.TotalAmount * 0.3m)
        {
            _logger.LogWarning("Guardrail: Vergi oranı mantıksız (InvoiceId: {Id})", invoice.Id);
            throw new InvalidOperationException("Vergi tutarı toplam tutarın %30'unu aşamaz. Olası okuma hatası.");
        }

        // 2. Gerçek Gemini API Bağlantısı
        var url = $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={_apiKey}";
        
        // Yapay zekaya ne yapması gerektiğini ve hangi formatta dönmesi gerektiğini söylüyoruz
        var prompt = $@"Aşağıdaki finansal metni analiz et ve kesinlikle sadece JSON formatında yanıt ver. 
        Metin: '{invoice.RawText}'
        Toplam Tutar: {invoice.TotalAmount}
        İstenen JSON formatı:
        {{
            ""SuggestedCategory"": 0, // 0: Yemek, 1: Seyahat, 2: OfisGideri, 3: Donanim, 4: Yazilim, 5: Tanimsiz
            ""ConfidenceScore"": 85, // 0-100 arasi bir güven skoru belirle
            ""Reasoning"": ""Neden bu kategori secildi kisa aciklama"",
            ""NeedsManualReview"": false
        }}";

        var requestBody = new
        {
            contents = new[] { new { parts = new[] { new { text = prompt } } } }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        
       if (!response.IsSuccessStatusCode)
{
    var errorContent = await response.Content.ReadAsStringAsync();
    throw new Exception($"Gemini API Hatası: {errorContent}");
}

        var responseString = await response.Content.ReadAsStringAsync();




        
        // 3. Gemini'den gelen yanıtı C# nesnesine dönüştürme (Deserialization)
        try
        {
            using var document = JsonDocument.Parse(responseString);
            var textResult = document.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text").GetString()?.Replace("```json", "").Replace("```", "").Trim();

            var aiResult = JsonSerializer.Deserialize<CategorizationResult>(textResult!);
            aiResult!.InvoiceId = invoice.Id;
            
            // Eğer yapay zeka kararsız kalırsa, manuel inceleme bayrağını kaldır
            if(aiResult.ConfidenceScore < 75) aiResult.NeedsManualReview = true;

            return aiResult;
        }
        catch (Exception ex)
        {
            _logger.LogError("AI yanıtı çözümlenemedi: {Message}", ex.Message);
            throw new Exception("Yapay zeka beklenmeyen bir formatta yanıt verdi.");
        }
    }
}