using Grupp4.Models;
using Grupp4.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<PlanetDBSettings>(builder.Configuration.GetSection("PlanetDB"));
builder.Services.AddSingleton<PlanetDBService>();

// Add services to the container.

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
