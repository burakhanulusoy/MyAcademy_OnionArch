// FluentValidation'ýn hata tipi (ValidationFailure) için gerekli
using FluentValidation.Results;
// ASP.NET Identity'nin hata tipi (IdentityError) için gerekli
using Microsoft.AspNetCore.Identity;

namespace ProjectApp.Application.Base
{
    // Tüm handler'larýn döndürdüðü ortak sonuç sýnýfý.
    // T: baþarýlý olduðunda geri döneceðimiz verinin tipi (örn. CategoryDto, List<ProductDto>).
    // Amaç: her iþlemin sonucunu ayný formatta döndürmek ? { data, errors, isSuccessful }
    public class BaseResult<T>
    {
        // Ýþlem baþarýlýysa dönecek veri.
        // T? ? hata durumunda veri olmayacaðý için null olabilir.
        // init ? sadece nesne oluþturulurken atanabilir, sonradan deðiþtirilemez.
        public T? Data { get; init; }

        // Ýþlem sýrasýnda oluþan hatalarýn listesi.
        // = new List<Error>() ? her BaseResult oluþturulduðunda otomatik boþ liste atanýr,
        //   böylece Errors hiçbir zaman null olmaz ("hata yok" = boþ liste).
        // IReadOnlyList ? dýþarýdan Add/Remove yapýlamaz, sadece okunabilir.
        // init ? liste sadece oluþturulurken verilebilir, sonradan baþka liste atanamaz.
        public IReadOnlyList<Error> Errors { get; init; } = new List<Error>();

        // Ýþlemin baþarýlý olup olmadýðýný hesaplar.
        // Hata listesi boþsa baþarýlý (true), içinde en az bir hata varsa baþarýsýz (false).
        // Errors asla null olmadýðý için null kontrolüne gerek yok.
        public bool IsSuccessful => Errors.Count == 0;


        // Baþarýlý ama geriye veri dönmeyen iþlemler için (Create, Update, Remove gibi).
        // Data null kalýr, Errors otomatik olarak boþ listedir ? IsSuccessful = true.
        public static BaseResult<T> Success()
        {
            return new BaseResult<T>();
        }

        // Baþarýlý ve geriye veri dönen iþlemler için (GetAll, GetById gibi).
        // Gelen veriyi Data'ya koyar, Errors yine boþ liste ? IsSuccessful = true.
        public static BaseResult<T> Success(T data)
        {
            return new BaseResult<T>
            {
                Data = data
            };
        }

        // FluentValidation hatalarýný kendi Error formatýmýza çevirir.
        // Parametre IEnumerable: validationResult.Errors IList olarak geliyor,
        // IEnumerable ise List, IList, dizi gibi tüm koleksiyonlarý kabul eder.
        public static BaseResult<T> Fail(IEnumerable<ValidationFailure> validationErrors)
        {
            // Her ValidationFailure'ý bir Error nesnesine dönüþtürüp listeye çeviriyoruz.
            var errors = (from error in validationErrors
                          select new Error
                          {
                              // Hata mesajý (örn. "Kategori adý boþ olamaz")
                              ErrorMessage = error.ErrorMessage,
                              // Hatanýn hangi alanla ilgili olduðu (örn. "Name")
                              PropertyName = error.PropertyName
                          }).ToList();

            // Hata listesiyle yeni bir sonuç döner ? Errors dolu olduðu için IsSuccessful = false.
            return new BaseResult<T>
            {
                Errors = errors
            };
        }

        // ASP.NET Identity hatalarýný (kayýt, giriþ, þifre kurallarý vb.) kendi Error formatýmýza çevirir.
        public static BaseResult<T> Fail(IEnumerable<IdentityError> identityErrors)
        {
            var errors = (from error in identityErrors
                          select new Error
                          {
                              // Identity'de mesaj Description alanýnda tutulur
                              // (örn. "Þifre en az bir büyük harf içermelidir")
                              ErrorMessage = error.Description,
                              // Identity'de alan adý yerine hata kodu var
                              // (örn. "PasswordRequiresUpper"), onu PropertyName olarak kullanýyoruz
                              PropertyName = error.Code
                          }).ToList();

            return new BaseResult<T>
            {
                Errors = errors
            };
        }

        // Tek bir özel hata mesajý döndürmek için (örn. "Kategori bulunamadý").
        // Belirli bir alana ait olmadýðý için PropertyName boþ býrakýlýr.
        public static BaseResult<T> Fail(string message)
        {
            return new BaseResult<T>
            {
                // Tek elemanlý bir hata listesi oluþturuyoruz
                Errors = new List<Error>
                {
                    new Error
                    {
                        ErrorMessage = message,
                        PropertyName = string.Empty
                    }
                }
            };
        }
    }

    // Tek bir hatayý temsil eden sýnýf.
    // Farklý kaynaklardan (FluentValidation, Identity, özel mesaj) gelen hatalarý
    // tek bir ortak formata çevirmek için kullanýlýr.
    public class Error
    {
        // Hatanýn ilgili olduðu alan (örn. "Name", "Email").
        // = string.Empty ? varsayýlan deðer, null olmasýný engeller.
        // init ? oluþturulduktan sonra deðiþtirilemez.
        public string PropertyName { get; init; } = string.Empty;

        // Kullanýcýya gösterilecek hata mesajý.
        public string ErrorMessage { get; init; } = string.Empty;
    }
}