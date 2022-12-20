using Grupp4.Models;
using Grupp4.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PlanetDBSettings>(builder.Configuration.GetSection("PlanetDB"));
builder.Services.AddSingleton<PlanetDBService>();

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Group 4 Api",
        Description = "This is a school project",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "AAMMM",
            Url = new Uri("https://aammm.com/contact")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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
