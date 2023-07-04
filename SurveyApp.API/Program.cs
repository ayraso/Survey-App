using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using SurveyApp.Application.Mappings;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Infrastructure.Data;
using System.ComponentModel.Design;
using SurveyApp.Application.Services.SurveyResponseService;
using SurveyApp.API.Extensions;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
//TODO: IEnumarable, IList gibi yapýlarý denetle.
// Add services to the container.
builder.Services.AddIoCServices();
builder.Services.LoadMongoDbSettings(builder.Configuration);
builder.Services.AddJWTAuthentication(builder.Configuration);

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
