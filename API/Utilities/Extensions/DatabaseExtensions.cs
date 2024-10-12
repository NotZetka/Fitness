using API.Data;
using API.Utilities.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Utilities.Extensions
{
    public static class DatabaseExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using DataContext dbContext =
                scope.ServiceProvider.GetRequiredService<DataContext>();

            dbContext.Database.Migrate();
        }

        public static async void AddIdentitiesToDb(this IApplicationBuilder app) 
        {
            var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<AppUserRole>>();
            var roles = new List<AppUserRole>
            {
                new AppUserRole{Name = RoleNames.Member},
                new AppUserRole{Name = RoleNames.Trainer}
            };

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role.Name) == null)
                    await roleManager.CreateAsync(role);
            }
        }

        public static void AddDatabaseWithIdentities(this IServiceCollection services, string connectionstring)
        {
            services.AddDbContext<DataContext>(options =>
                   options.UseSqlServer(connectionstring));
            services.AddIdentityCore<AppMember>()
                .AddRoles<AppUserRole>()
                .AddRoleManager<RoleManager<AppUserRole>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            services.AddIdentityCore<AppTrainer>()
                .AddRoles<AppUserRole>()
                .AddRoleManager<RoleManager<AppUserRole>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
        }

    }
}
