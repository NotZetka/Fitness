using API.Data.Dtos;

namespace API.Data.Repositories
{
    public interface IBodyWeightRepository : IRepository<BodyWeight>
    {
        public void SetHeight(int height, int userId);
        public void AddRecord(int userId, BodyWeightRecord record);

        public Task<BodyWeightDto> GetBodyWeightAsync(int userId);
    }
}
