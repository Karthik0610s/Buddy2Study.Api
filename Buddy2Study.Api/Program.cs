using Buddy2Study.Application.Interfaces;
using Buddy2Study.Application.Mappings;
using Buddy2Study.Application.Services;
using Buddy2Study.Infrastructure.DatabaseConnection;
using Buddy2Study.Infrastructure.Interfaces;
using Buddy2Study.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Models;

using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add AutoMapper and configure mapping profiles
services.AddAutoMapper(typeof(MappingProfile));
services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
//services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IScholarshipApplicationFormRepository, ScholarshipApplicationFormRepository>();
services.AddScoped<IScholarshipApplicationFormService, ScholarshipApplicationFormService>();
services.AddScoped<IDataBaseConnection, DataBaseConnection>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<ISponsorService, SponsorService>();
services.AddScoped<ISponsorRepository, SponsorRepository>();
services.AddScoped<IInstitutionService, InstitutionService>();
services.AddScoped<IInstitutionRepository, InstitutionRepository>();
services.AddScoped<IStudentRepository, StudentRepository>();
services.AddScoped<IStudentService, StudentService>();
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

services.AddLocalization();
services.AddMvc();
services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTSettings:SecretKey"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTSettings:Audience"]
    };
});
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Funds4Education.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Funds4Education.API Authorization",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funds4Education.Service.API v1"));

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyAllowSpecificOrigins");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

