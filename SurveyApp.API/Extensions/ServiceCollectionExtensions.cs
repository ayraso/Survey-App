using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SurveyApp.Application.Mappings;
using SurveyApp.Application.Services.CachingServices;
using SurveyApp.Application.Services.SurveyAnalyzer;
using SurveyApp.Application.Services.SurveyResponseService;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Infrastructure.Data;
using System.Text;
using ISurveyAnalyzer = SurveyApp.Application.Services.SurveyAnalyzer.ISurveyAnalyzer;

namespace SurveyApp.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIoCServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<ISurveyResponseService, SurveyResponseService>();
            services.AddScoped<ISurveyAnalyzer, SurveyAnalyzer>();
            services.AddScoped<IMemoryCacheService, MemoryCacheService>();

            services.AddAutoMapper(typeof(MappingProfile));
        }
        public static void LoadMongoDbSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
        }
        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    // secret key
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtKey").ToString())),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
