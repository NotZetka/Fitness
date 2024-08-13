namespace API.Data.Repositories
{
    public interface IRepository<T> where T : DbEntity
    {
        void Add(T entity);

        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void DeleteById(int id);
        void Delete(T entity);
        IList<T> GetAll();
        Task<IList<T>> GetAllAsync();
    }
}
