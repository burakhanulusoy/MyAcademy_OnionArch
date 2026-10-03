namespace ProjectApp.Application.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {

        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);


    }
}
