using Microsoft.AspNetCore.Http; // IFormFile bu namespace'te

namespace ProjectApp.Application.Services.FileServices;

// Sözleşme: "Dosya servisi olan her class bu iki metodu yazmak zorunda."
// Handler'lar class'ı değil bu interface'i kullanır.
// Böylece ileride diske değil de buluta (Azure, AWS) kaydetmek istersen
// handler'lara dokunmadan sadece yeni bir class yazıp registration'ı değiştirirsin.
public interface IFileService
{
    // Dosyayı verilen klasöre kaydeder, veritabanına yazılacak yolu döndürür.
    // file   → kullanıcının yüklediği dosya
    // folder → "Product", "Banner" gibi alt klasör adı
    // dönüş  → "/Images/Product/abc.jpg" gibi kısa yol
    Task<string> UploadAsync(IFormFile file, string folder);

    // Veritabanındaki kısa yolu alıp diskteki dosyayı siler.
    // string? → null gelebilir (görseli olmayan ürün)
    void Delete(string? fileUrl);
}