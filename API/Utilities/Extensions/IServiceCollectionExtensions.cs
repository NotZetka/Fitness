using API.Data.Repositories;
using API.Services;
using API.SignalR;

namespace API.Utilities.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<PresenceTracker>();

            return services;
        }
    }
}
