﻿using API.Data;
using API.Data.Dtos;
using API.Handlers.Accounts.Register;
using API.Handlers.Accounts.Register.Member;
using API.Handlers.Accounts.Register.Trainer;
using API.Handlers.BodyWeight.AddBodyWeightRecord;
using AutoMapper;

namespace API.Utilities
{
    public class MappingProlife : Profile
    {
        public MappingProlife()
        {
            CreateMap<AppMember, UserDto>();
            CreateMap<ExerciseTemplateDto, ExerciseTemplate>();
            CreateMap<ExerciseTemplate, ExerciseTemplateDto>();
            CreateMap<FitnessPlan, FitnessPlanDto>();
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<Record,  RecordDto>();
            CreateMap<Message, MessageDto>();
            CreateMap<AddBodyWeightRecordCommand, BodyWeightRecord>();
            CreateMap<BodyWeight,BodyWeightDto>();
            CreateMap<BodyWeightRecord, BodyWeightRecordDto>();
            CreateMap<RegisterCommand, RegisterTrainerCommand>();
            CreateMap<RegisterCommand, RegisterMemberCommand>();
        }
    }
}
