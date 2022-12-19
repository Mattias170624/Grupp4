
using Grupp4.Services;
using Grupp4.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MongoDB.Driver;

// Create application builder (see developer notes at end of file)
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("SampleTrainingDb"));
builder.Services.AddSingleton<GradeService>();

builder.Services.AddControllers();
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