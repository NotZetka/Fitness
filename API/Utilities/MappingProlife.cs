using API.Data;
using API.Data.Dtos;
using AutoMapper;

namespace API.Utilities
{
    public class MappingProlife : Profile
    {
        public MappingProlife()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<ExerciseTemplateDto, ExerciseTemplate>();
            CreateMap<ExerciseTemplate, ExerciseTemplateDto>();
            CreateMap<FitnessPlan, FitnessPlanDto>();
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<Record,  RecordDto>();
            CreateMap<Message, MessageDto>();
        }
    }
}
