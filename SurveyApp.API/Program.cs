using SurveyApp.API.Models;
using SurveyApp.Application.Mappings;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// düzenlenecek
builder.Services.AddSingleton<IUserService, UserService>();
// MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
