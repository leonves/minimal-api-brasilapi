using AutoMapper;
using BrasilApiIntegration.Configuration;
using BrasilApiIntegration.Data;
using BrasilApiIntegration.Data.Entities;
using BrasilApiIntegration.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddScoped<WeatherService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapperSetup();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Brasil API Integration", Version = "v1" });
});

#region Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});
#endregion

var app = builder.Build();

#region Controllers
app.MapGet("/weather/aeroporto/{icaoCode}", async (WeatherService weatherService, string icaoCode, IServiceProvider serviceProvider) =>
{
    var weatherResponse = await weatherService.GetWeatherForAirportAsync(icaoCode);
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    var weatherEntities = mapper.Map<Weather>(weatherResponse);

    var saveResult = await weatherService.SaveWeatherAsync(weatherEntities);

    if (saveResult.IsSuccessful)
    {
        return Results.Ok(weatherEntities);
    }
    else
    {
        return Results.BadRequest(saveResult.ErrorMessage);
    }

});

app.MapGet("/weather/capital", async (WeatherService weatherService, IServiceProvider serviceProvider) =>
{
    var weatherResponses = await weatherService.GetWeatherForCapitalAsync();
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    var weatherEntities = mapper.Map<List<Weather>>(weatherResponses);

    var saveResult = await weatherService.SaveWeatherListAsync(weatherEntities);

    if (saveResult.IsSuccessful)
    {
        return Results.Ok(weatherResponses);
    }
    else
    {
        return Results.BadRequest(saveResult.ErrorMessage);
    }
});
#endregion

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Brasil Api Integration V1");
});


app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }

    await next();
});

app.Run();
