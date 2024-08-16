using API.Data;
using API.Data.Dtos;
using API.Handlers.BodyWeight.AddBodyWeightRecord;
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
            CreateMap<AddBodyWeightRecordCommand, BodyWeightRecord>();
            CreateMap<BodyWeight,BodyWeightDto>();
            CreateMap<BodyWeightRecord, BodyWeightRecordDto>();
        }
    }
}
