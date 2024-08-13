
using API.Data.Dtos;
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

        public async Task<BodyWeightDto> GetBodyWeightAsync(int userId)
        {
            return await _dbSet
                .Where(x=>x.UserId == userId)
                .ProjectTo<BodyWeightDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public void SetHeight(int height, int userId)
        {
            var bodyWeight = _dbSet.FirstOrDefault(x => x.UserId == userId);

            bodyWeight.Height = height;
        }
    }
}
