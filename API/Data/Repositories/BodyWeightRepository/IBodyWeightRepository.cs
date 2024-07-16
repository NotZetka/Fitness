using API.Data.Dtos;
using API.Handlers.BodyWeight.AddBodyWeightRecord;

namespace API.Data.Repositories.BodyWeightRepository
{
    public interface IBodyWeightRepository : IRepository
    {
        public void SetHeight(int height, int userId);
        public void AddRecord(int userId, BodyWeightRecord record);

        public Task<BodyWeightDto> GetBodyWeightAsync(int userId);
    }
}
