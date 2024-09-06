using API.Data.Dtos;
using API.Utilities;

namespace API.Data.Repositories
{
    public interface IBodyWeightRepository : IRepository<BodyWeight>
    {
        public void SetHeight(int height, int userId);
        public void AddRecord(int userId, BodyWeightRecord record);

        public Task<int?> GetHeight(int userId);

        public Task<PagedResult<BodyWeightRecordDto>> GetBodyWeightRecordsAsync(int userId, int? pageNumber = null, int? pageSize = null);
    }
}
