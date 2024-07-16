
using API.Data.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.BodyWeightRepository
{
    public class BodyWeightRepository : AbstractRepository, IBodyWeightRepository
    {
        private readonly IMapper _mapper;

        public BodyWeightRepository(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void AddRecord(int userId, BodyWeightRecord record)
        {
            var bodyWeight = _context.BodyWeights.First(x=>x.UserId == userId);

            bodyWeight.WeightRecords.Add(record);
        }

        public async Task<BodyWeightDto> GetBodyWeightAsync(int userId)
        {
            return await _context.BodyWeights
                .Where(x=>x.UserId == userId)
                .ProjectTo<BodyWeightDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public void SetHeight(int height, int userId)
        {
            var bodyWeight = _context.BodyWeights.FirstOrDefault(x => x.UserId == userId);

            bodyWeight.Height = height;
        }
    }
}
