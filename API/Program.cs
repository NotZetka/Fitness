using API.Database;
using API.Handlers.Accounts.Login;
using API.Handlers.Accounts.Register;
using API.Middleware;
using API.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityCore<AppUser>()
    .AddRoles<AppUserRole>()
    .AddRoleManager<RoleManager<AppUserRole>>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<IValidator<LoginQuery>, LoginQueryValidator>();
builder.Services.AddTransient<IValidator<RegisterQuery>, RegisterQueryValidator>();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    opt.AddPolicy("RequireModeratorRole", policy => policy.RequireRole("Admin", "Moderator"));
});


var app = builder.Build();

app.UseMiddleware<ExceptionhandlingMiddleware>();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseCors(opt => opt
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200")
    );
app.MapControllers();

var roleManager = app.Services.CreateScope().ServiceProvider.GetRequiredService<RoleManager<AppUserRole>>();
var roles = new List<AppUserRole>
            {
                new AppUserRole{Name = "Member"},
                new AppUserRole{Name = "Admin"},
                new AppUserRole{Name = "Moderator"},
            };

foreach (var role in roles)
{
    if(await roleManager.FindByNameAsync(role.Name) == null)
        await roleManager.CreateAsync(role);
}

app.Run();
