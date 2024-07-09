namespace API.Data.Repositories.UsersRepository
{
    public interface IUsersRepository : IRepository
    {
        Task<AppUser> FindUserByEmailAsync(string email);
        Task<AppUser> FindUserByUsernamelAsync(string email);
    }
}
