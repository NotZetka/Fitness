﻿using API.Handlers.Accounts.Login;
using API.Handlers.Accounts.Register;
using API.Handlers.BodyWeight.AddBodyWeightRecord;
using API.Handlers.BodyWeight.SetHeight;
using API.Handlers.Messages.GetMessageThread;
using API.Handlers.Messages.SendMessage;
using API.Handlers.Plans.AddPlan;
using API.Handlers.Plans.AddRecord;
using API.Handlers.Plans.AddRecords;
using API.Handlers.Plans.Publish;
using API.Middleware;
using FluentValidation;
using MediatR;

namespace API.Utilities.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddValidatiors(this IServiceCollection services) {

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<IValidator<LoginQuery>, LoginQueryValidator>();
            services.AddTransient<IValidator<RegisterQuery>, RegisterQueryValidator>();
            services.AddTransient<IValidator<PublishPlanQuery>, PublicPlanQueryValidator>();
            services.AddTransient<IValidator<AddPlanQuery>, AddPlanQueryValidator>();
            services.AddTransient<IValidator<AddRecordsQuery>, AddRecordsQueryValidator>();
            services.AddTransient<IValidator<SendMessageQuery>, SendMessageQueryValidator>();
            services.AddTransient<IValidator<GetMessageThreadQuery>, GetMessageThreadQueryValidator>();
            services.AddTransient<IValidator<AddBodyWeightRecordQuery>, AddBodyweightQueryValidator>();
            services.AddTransient<IValidator<SetHeightQuery>, SetHeightQueryValidator>();

            return services;
        }
    }
}
