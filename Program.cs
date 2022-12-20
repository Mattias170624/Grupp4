using Grupp4.Models;
using Grupp4.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PlanetDBSettings>(builder.Configuration.GetSection("PlanetDB"));
builder.Services.AddSingleton<PlanetDBService>();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbService>();

builder.Services.Configure<GradeDbSettings>(builder.Configuration.GetSection("SampleTrainingDb"));
builder.Services.AddSingleton<GradeService>();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Grupp 4 API",
        Description = "An ASP.NET Core Web API for managing Movies, Restaurant and sales",
        TermsOfService = new Uri("https://ecample.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Grupp1",
            Url = new Uri("https://example.com/contact"),

        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // Use Reflection to build XML-filename matching of the web API project.
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

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
