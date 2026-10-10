using ProjectApp.WebUI.Base;               // ApiResult bu namespace'te
using ProjectApp.WebUI.DTOs.CategoryDtos;  // Kategori command/result record'ları burada

namespace ProjectApp.WebUI.Services.CategoryServices
{
    // ICategoryService sözleşmesini uygulayan class.
    // _client → Extension'daki AddHttpClient sayesinde DI'dan otomatik gelir.
    //           BaseAddress'i zaten ayarlı: https://localhost:7256/api/
    //           Biz new HttpClient() yazmıyoruz, bağlantıları ASP.NET Core yönetiyor.
    public class CategoryService(HttpClient _client) : ICategoryService
    {
        // ==================== TÜM KATEGORİLER ====================
        public async Task<ApiResult<List<GetCategoriesQueryResult>>> GetAllAsync()
        {
            // API'ye GET isteği at.
            // BaseAddress + "categories" → GET https://localhost:7256/api/categories
            // response → gelen cevabın tamamı (durum kodu: 200/400, header'lar, gövde)
            var response = await _client.GetAsync("categories");

            // response.Content     → cevabın gövdesi, yani JSON yazısı
            // ReadFromJsonAsync<>  → JSON'u verdiğimiz tipe çevirir (deserialize: JSON → C#)
            //                        "data" → Data, "errors" → Errors, "isSuccessful" → IsSuccessful
            // List<...>            → API birden fazla kategori döndüğü için liste
            // ?? new()             → okuma null dönerse (API çöktü, boş cevap) boş ApiResult döndür,
            //                        sayfa patlamasın. Boş ApiResult'ta IsSuccessful = false olur.
            return await response.Content.ReadFromJsonAsync<ApiResult<List<GetCategoriesQueryResult>>>() ?? new();
        }

        // ==================== TEK KATEGORİ ====================
        public async Task<ApiResult<GetCategoryByIdQueryResult>> GetByIdAsync(int id)
        {
            // $"..." → içine değişken koyar. id = 5 ise → GET .../api/categories/5
            // GetFromJsonAsync KULLANMIYORUZ: kategori bulunamazsa API 400 döner,
            // GetFromJsonAsync 400 görünce JSON'u okumadan exception fırlatır.
            // GetAsync ise durum kodu ne olursa olsun cevabı bize verir.
            var response = await _client.GetAsync($"categories/{id}");

            // Başarılıysa  → Data dolu, IsSuccessful = true
            // Bulunamadıysa → Data null, Errors'ta "kategori bulunamadı" mesajı, IsSuccessful = false
            // Tek kategori olduğu için List değil, direkt GetCategoryByIdQueryResult
            return await response.Content.ReadFromJsonAsync<ApiResult<GetCategoryByIdQueryResult>>() ?? new();
        }

        // ==================== EKLEME ====================
        public async Task<ApiResult<object>> CreateAsync(CreateCategoryCommand command)
        {
            // PostAsJsonAsync → command nesnesini JSON'a çevirir (serialize: C# → JSON)
            //                   ve POST .../api/categories olarak gönderir.
            // CreateCategoryCommand("Giyim") → { "name": "Giyim" }
            // API tarafında bu JSON, API'deki CreateCategoryCommand'a dolar
            // (property isimleri aynı olduğu için eşleşiyor).
            var response = await _client.PostAsJsonAsync("categories", command);

            // API'nin cevabını oku.
            // ApiResult<object> → ekleme işleminde API data göndermiyor,
            //                     bizi sadece IsSuccessful ve Errors ilgilendiriyor.
            // Validasyon hatası varsa (ör. "Kategori adı boş olamaz") Errors'a düşer.
            return await response.Content.ReadFromJsonAsync<ApiResult<object>>() ?? new();
        }

        // ==================== GÜNCELLEME ====================
        public async Task<ApiResult<object>> UpdateAsync(UpdateCategoryCommand command)
        {
            // Create ile aynı mantık, sadece HTTP metodu PUT.
            // API'de [HttpPut] (Id'siz) olduğu için adres yine "categories".
            // Güncellenecek Id URL'de değil, JSON'un içinde gidiyor:
            // UpdateCategoryCommand(3, "Yeni Ad") → { "id": 3, "name": "Yeni Ad" }
            var response = await _client.PutAsJsonAsync("categories", command);

            return await response.Content.ReadFromJsonAsync<ApiResult<object>>() ?? new();
        }

        // ==================== SİLME ====================
        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            // DELETE .../api/categories/5
            // Gövdeye bir şey koymuyoruz, Id URL'de gidiyor.
            // API'de [HttpDelete("{id}")] olduğu için böyle eşleşiyor.
            var response = await _client.DeleteAsync($"categories/{id}");

            // Silme başarılı mı, değilse neden (ör. "kategori bulunamadı") → ApiResult'tan öğreniyoruz.
            return await response.Content.ReadFromJsonAsync<ApiResult<object>>() ?? new();
        }
    }
}