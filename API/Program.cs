using API.Middleware;
using API.Services;
using API.Utilities.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseWithIdentities(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddRepositories();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
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
builder.Services.AddValidatiors();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    opt.AddPolicy("RequireModeratorRole", policy => policy.RequireRole("Admin", "Moderator"));
});


var app = builder.Build();

app.ApplyMigrations();
await app.AddIdentitiesToDb();
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

app.Run();
