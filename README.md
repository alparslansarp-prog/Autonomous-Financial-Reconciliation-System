# 🚀 Otonom Finansal Veri Sınıflandırma ve Mutabakat Sistemi

Yapay zeka muhakeme yeteneği (Gemini API) ve modern .NET 10 mimarisi kullanılarak geliştirilmiş, otonom finansal veri işleme ve mutabakat projesidir. İstemci-Sunucu (Client-Server) mimarisiyle tasarlanan sistem, geleneksel manuel veri giriş süreçlerini akıllı ve otonom bir akışa dönüştürür.

## 🎯 Proje Vizyonu ve Kullanım Alanı
Finans ve muhasebe departmanlarındaki rutin evrak işlerini (fatura, fiş, taşeron hakediş belgeleri) manuel işlemek hem zaman kaybına hem de insan kaynaklı hatalara yol açar. Bu proje, özellikle **inşaat ve taahhüt** gibi karmaşık gider kalemlerinin (hakedişler, avanslar, spesifik KDV oranları) bulunduğu sektörlerde, verilerin doğrudan yapay zeka tarafından okunup bağlamsal olarak sınıflandırılmasını hedefler.

Yapay zeka sadece anahtar kelime eşleştirmesi yapmaz; metnin bütününü analiz ederek işlemin "neden" o kategoriye ait olduğunu finansal bir dille **gerekçelendirir**.

## ⚙️ Temel Özellikler
*   **Otonom Sınıflandırma:** OCR ile dijitalleştirilmiş belgeleri (Yemek, Seyahat, Ofis Gideri, Donanım, Yazılım, Tanımsız) kategorilerine otomatik ayırır.
*   **Bağlamsal Muhakeme (Reasoning):** Yapay zeka, standart dışı belgeleri (örn: personel işe giriş evrakları veya taşeron hakedişleri) analiz eder, "%95 Güven Skoru" ve mantıksal bir açıklama ile "Tanımsız" veya ilgili kategoriye atayarak insan onayına (Manual Review) sunar.
*   **Ayrıştırılmış Mimari:** Blazor WebAssembly ile ön yüz (UI) ve ASP.NET Core Web API ile arka plan (Backend) tamamen izole çalışır.
*   **AI Guardrails (Güvenlik Bariyerleri):** API yanıt vermediğinde veya geçersiz format döndüğünde sistemi koruyan özel iş kuralları ve hata yakalama mekanizmaları.

## 🛠️ Kullanılan Teknolojiler
*   **Backend:** .NET 10, ASP.NET Core Web API, Entity Framework Core (In-Memory DB)
*   **Frontend:** Blazor WebAssembly (WASM), C#
*   **Yapay Zeka:** Google Gemini 1.5 Pro API 
*   **Geliştirme Yaklaşımı:** Vibecoding (AI destekli otonom kod mimarisi inşası) ve Clean Architecture prensipleri.

## 🚀 Kurulum ve Çalıştırma

Projeyi lokalinizde çalıştırmak için aşağıdaki adımları izleyin:

**1. API Anahtarınızı Tanımlayın**
`OtonomFinansAPI` projesi içindeki `appsettings.json` dosyasına gidin ve kendi Google Gemini API anahtarınızı ekleyin:
```json
"GeminiApiKey": "SİZİN_API_ANAHTARINIZ_BURAYA"