
using API.Data.Dtos;
using API.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class BodyWeightRepository(DataContext context, IMapper _mapper) : AbstractRepository<BodyWeight>(context), IBodyWeightRepository
    {
        public void AddRecord(int userId, BodyWeightRecord record)
        {
            var bodyWeight = _dbSet.First(x=>x.UserId == userId);

            bodyWeight.WeightRecords.Add(record);
        }

        public async Task<PagedResult<BodyWeightRecordDto>> GetBodyWeightRecordsAsync(int userId, int? pageNumber = null, int? pageSize = null)
        {
            var query = _dbSet
                .Where(x => x.UserId == userId)
                .SelectMany(x => x.WeightRecords)
                .ProjectTo<BodyWeightRecordDto>(_mapper.ConfigurationProvider);

            if (pageNumber != null && pageSize != null)
            {
                return await PagedResult<BodyWeightRecordDto>.CreateFromQueryAsync(query, pageNumber.Value, pageSize.Value);
            }

            return new PagedResult<BodyWeightRecordDto>(await query.ToListAsync());
        }

        public async Task<int?> GetHeight(int userId)
        {
            return await _dbSet
                .Where(x => x.UserId == userId)
                .Select(x => x.Height)
                .FirstOrDefaultAsync();
        }

        public void SetHeight(int height, int userId)
        {
            var bodyWeight = _dbSet.FirstOrDefault(x => x.UserId == userId);

            bodyWeight.Height = height;
        }
    }
}
