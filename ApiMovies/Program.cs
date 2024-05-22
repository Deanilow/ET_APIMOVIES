using ApiMovies;
using ApiMovies.Background;
using ApiMovies.Common;
using ApiMovies.Common.Interfaces;
using ApiMovies.Common.Service;
using ApiMovies.Data.Initialization;
using ApiMovies.Logger;
using ApiMovies.Routes;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text.Json.Serialization;

var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", true, true)
           .Build();

var dotnetEnvironment = configuration["ENVIRONMENT"];

Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", dotnetEnvironment ?? "Development");

var builder = WebApplication.CreateBuilder(args);

builder.UseSerilogConfig();
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", false, true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0.1",
        Title = "Minimal Api Movies ",
        Description = "Minimal Api Movies V1",
        Contact = new OpenApiContact
        {
            Name = "Danilo Ramos",
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "corsApiMovies",
    policy =>
    {
        policy.WithMethods("GET");
        policy.AllowAnyOrigin();
    });
});


builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSqlServer<DBContext>(
    builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var applicationSettings = builder.Configuration.GetSection("AppSettings").Get<Settings>();

builder.Services.AddSingleton<ISettings, Settings>(e => applicationSettings);
builder.Services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
builder.Services.AddTransient<IMovieHttps, MovieHttps>();
builder.Services.AddTransient<ApplicationDbInitializer>();
builder.Services.AddTransient<ApplicationDbSeeder>();
builder.Services.AddHostedService<UpdateDataBackgroundService>();

var app = builder.Build();

await app.Services.InitializeDatabasesAsync();

TrendingRoutes.RegisterTrendingApi(app);
PopularRoutes.RegisterPopularApi(app);
SearchRoutes.RegisterSearchApi(app);
HomeRoutes.RegisterHomeApi(app);

app.UseStatusCodePages(context =>
{
    var request = context.HttpContext.Request;
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.NotFound
    && !request.Path.Value.Contains("/ApiMovies"))
    {
        response.Redirect("/");
    }

    return Task.CompletedTask;
});

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("corsApiMovies");
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
