
namespace API.Data.Repositories
{
    public abstract class AbstractRepository : IRepository
    {
        protected readonly DataContext _context;

        protected AbstractRepository(DataContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
