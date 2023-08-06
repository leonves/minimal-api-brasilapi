using BrasilApiIntegration.Services;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Brasil API Integration", Version = "v1" });
});

var app = builder.Build();

#region Controllers
app.MapGet("/weather/aeroporto/{icaoCode}", async (WeatherService weatherService, string icaoCode) =>
{
    var response = await weatherService.GetWeatherForAirportAsync(icaoCode);

    // Salvar a resposta no banco de dados aqui

    return response;
});

app.MapGet("/weather/capital", async (WeatherService weatherService) =>
{
    var response = await weatherService.GetWeatherForCapitalAsync();

    // Salvar a resposta no banco de dados aqui

    return response;
});
#endregion

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
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
