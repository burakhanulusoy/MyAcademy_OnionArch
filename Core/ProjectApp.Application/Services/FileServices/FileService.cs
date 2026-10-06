using Microsoft.AspNetCore.Hosting; // IWebHostEnvironment bu namespace'te
using Microsoft.AspNetCore.Http;    // IFormFile bu namespace'te

namespace ProjectApp.Application.Services.FileServices;

// IFileService sözleþmesini uygulayan gerçek class.
// _env ? DI'dan otomatik gelir, "uygulama diskte nerede çalýþýyor" bilgisini verir.
//        Biz new'lemeyiz, ASP.NET Core kendisi kaydeder ve buraya verir.
public class FileService(IWebHostEnvironment _env) : IFileService
{
    // wwwroot klasörünün tam yolu. Örn: C:\...\ProjectApp.API\wwwroot
    // ?? ? soldaki null ise saðdakini kullan.
    // wwwroot yoksa WebRootPath null gelir, o zaman proje klasörü + "wwwroot" yolunu biz oluþtururuz.
    private string WebRoot => _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");

    public async Task<string> UploadAsync(IFormFile file, string folder)
    {
        // Dosya adýndan uzantýyý al ve küçük harfe çevir.
        // "Kazak.JPG" ? ".jpg"
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        // Kaydedilecek klasörün tam yolunu oluþtur.
        // Path.Combine ? \ veya / ayracýný iþletim sistemine göre kendisi koyar.
        // Sonuç: C:\...\wwwroot\Images\Product
        var folderPath = Path.Combine(WebRoot, "Images", folder);

        // Klasör yoksa oluþturur, varsa hiçbir þey yapmaz.
        // "Banner" gönderilirse Images\Banner burada kendiliðinden açýlýr.
        Directory.CreateDirectory(folderPath);

        // Dosyaya benzersiz yeni isim ver: "3f2a9c1e-...-b7d4.jpg"
        // Kullanýcýnýn dosya adýný kullanmýyoruz çünkü:
        //  - ayný isimli iki dosya birbirinin üstüne yazar
        //  - isimde Türkçe karakter, boþluk gibi sorunlu þeyler olabilir
        var fileName = $"{Guid.NewGuid()}{extension}";

        // Diskte boþ bir dosya aç (FileMode.Create = oluþtur).
        // await using ? iþ bitince dosyayý kapatýr; kapatmazsak dosya kilitli kalýr.
        await using var stream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create);

        // Yüklenen görselin içeriðini açtýðýmýz dosyaya kopyala.
        await file.CopyToAsync(stream);

        // Veritabanýna tam disk yolunu deðil kýsa yolu yazýyoruz.
        // Tarayýcý görseli https://localhost:xxxx/Images/Product/... adresinden açacak,
        // proje baþka bilgisayara taþýnsa da bu yol deðiþmez.
        return $"/Images/{folder}/{fileName}";
    }

    public void Delete(string? fileUrl)
    {
        // Görsel yolu boþsa silinecek bir þey yok, metottan çýk.
        if (string.IsNullOrWhiteSpace(fileUrl)) return;

        // Kýsa yolu tekrar tam disk yoluna çevir (UploadAsync'teki return'ün tersi):
        // "/Images/Product/abc.jpg"
        //   ? TrimStart('/')  ? "Images/Product/abc.jpg"   (baþtaki / atýlmazsa Path.Combine WebRoot'u yok sayar)
        //   ? Replace('/', \) ? "Images\Product\abc.jpg"   (Windows ayracýna çevir)
        //   ? Path.Combine    ? "C:\...\wwwroot\Images\Product\abc.jpg"
        var fullPath = Path.Combine(WebRoot, fileUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

        // Dosya gerçekten varsa sil. Kontrol etmezsek, dosya elle silinmiþse hata patlar.
        if (File.Exists(fullPath)) File.Delete(fullPath);
    }
}