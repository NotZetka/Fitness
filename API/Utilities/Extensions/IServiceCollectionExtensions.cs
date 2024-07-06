using API.Handlers.Accounts.Login;
using API.Handlers.Accounts.Register;
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

            return services;
        }
    }
}
