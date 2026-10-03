using ProjectApp.Application.Contracts;
using ProjectApp.Persistence.Context;

namespace ProjectApp.Persistence.Concrete
{
    public class UnitOfWork(AppDbContext _context) : IUnitOfWork
    {
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;   // savechanges bize etkilenen satýr sayýsýný döndürür. 0 dan büyükse true dönmüþtür. 0 ise false döner. yani deðiþiklik yok demektir. savechanges sonunda ... rows affected gibi bir mesaj döner. bu mesajý kullanmak için >0 ile kontrol ediyoruz.

        }
    }
}
