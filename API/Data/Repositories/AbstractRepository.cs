
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class AbstractRepository<T> : IRepository<T> where T: DbEntity
    {
        protected readonly DbSet<T> _dbSet;

        protected AbstractRepository(DataContext context) 
        {
            _dbSet = context.Set<T>();
        }

        public void Add(T entity) 
        {
            _dbSet.Add(entity);
        }

        public virtual T GetById(int id)
        {
            return _dbSet.FirstOrDefault(x=>x.Id == id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x=>x.Id == id);
        }
        public void Delete(T entity)
        {
            _dbSet.Where(x=>x.Id == entity.Id).ExecuteDelete();
        }

        public void DeleteById(int id)
        {
            _dbSet.Where(x => x.Id == id).ExecuteDelete();
        }
        public virtual IList<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
