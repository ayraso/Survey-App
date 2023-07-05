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
using Microsoft.AspNetCore.Identity;
using SurveyApp.Application.Services.Common;

var builder = WebApplication.CreateBuilder(args);
//TODO: IEnumarable, IList gibi yapýlarý denetle.
//TODO: Authorization için middleware ekle.
//TODO: anket sonuçlarýný ve analizini talep eden kiþi anketin sahibi veya admin mi diye kontrol et.
// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddIoCServices();
builder.Services.LoadMongoDbSettings(builder.Configuration);
builder.Services.AddJWTAuthentication(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCaching(options =>
{
    options.SizeLimit = 100000;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
