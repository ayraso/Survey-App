using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using SurveyApp.API.Models;
using SurveyApp.Application.Mappings;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Infrastructure.Data;
using System.ComponentModel.Design;
using SurveyApp.Application.Services.SurveyResponseService;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// düzenlenecek
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ISurveyService, SurveyService>();
builder.Services.AddSingleton<ISurveyResponseService, SurveyResponseService>();
// MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var objectSerializer = new ObjectSerializer(type => ObjectSerializer.DefaultAllowedTypes(type) || type.FullName.StartsWith("SurveyApp"));
BsonSerializer.RegisterSerializer(objectSerializer);

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
