namespace ProjectApp.Application.Contracts
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();

    }
}
