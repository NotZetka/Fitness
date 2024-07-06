using API.Data;
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

        public static async Task AddIdentitiesToDb(this IApplicationBuilder app) 
        {
            var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<AppUserRole>>();
            var roles = new List<AppUserRole>
            {
                new AppUserRole{Name = "Member"},
                new AppUserRole{Name = "Admin"},
                new AppUserRole{Name = "Moderator"},
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
            services.AddIdentityCore<AppUser>()
                .AddRoles<AppUserRole>()
                .AddRoleManager<RoleManager<AppUserRole>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
        }

    }
}
