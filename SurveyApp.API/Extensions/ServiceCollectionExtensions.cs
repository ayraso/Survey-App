using SurveyApp.Application.Mappings;
using SurveyApp.Application.Services.SurveyResponseService;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Infrastructure.Data;

namespace SurveyApp.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIoCServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<ISurveyResponseService, SurveyResponseService>();

            services.AddAutoMapper(typeof(MappingProfile));
        }
        public static void LoadMongoDbSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
        }
    }
}
