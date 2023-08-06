using AutoMapper;
using BrasilApiIntegration.Data;
using BrasilApiIntegration.Data.Entities;
using BrasilApiIntegration.Model;
using BrasilApiIntegration.Model.Response;

namespace BrasilApiIntegration.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public WeatherService(HttpClient httpClient, IMapper mapper, AppDbContext dbContext)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<WeatherResponse> GetWeatherForAirportAsync(string icaoCode)
        {
            return await _httpClient.GetFromJsonAsync<WeatherResponse>(
                $"https://brasilapi.com.br/api/cptec/v1/clima/aeroporto/{icaoCode}");
        }

        public async Task<List<WeatherResponse>> GetWeatherForCapitalAsync()
        {

            return await _httpClient.GetFromJsonAsync<List<WeatherResponse>>(
                "https://brasilapi.com.br/api/cptec/v1/clima/capital");

        }
        public async Task<ServiceResult> SaveWeatherListAsync(List<Weather> weatherList)
        {
            try
            {
                _dbContext.Weathers.AddRange(weatherList);
                await _dbContext.SaveChangesAsync();

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                var logEntry = new Log(ex.Message);
                _dbContext.Logs.Add(logEntry);
                await _dbContext.SaveChangesAsync();

                return ServiceResult.Error(ex.Message);
            }
        }

        public async Task<ServiceResult> SaveWeatherAsync(Weather weather)
        {
            try
            {
                _dbContext.Weathers.Add(weather);
                await _dbContext.SaveChangesAsync();

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                var logEntry = new Log(ex.Message);
                _dbContext.Logs.Add(logEntry);
                await _dbContext.SaveChangesAsync();

                return ServiceResult.Error(ex.Message);
            }
        }
    }
}
