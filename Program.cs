using Grupp4.Models;
using Grupp4.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<MongoDbService>();




// Add services to the container.
/*

// add mongoclient
MongoClient client_Amir = new MongoClient("mongodb+srv://Amir:wjbn7WrAW3m5RrpU@amircluster.bux2ihe.mongodb.net/?retryWrites=true&w=majority");

List<string> databases = client_Amir.ListDatabaseNames().ToList();
*/

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
