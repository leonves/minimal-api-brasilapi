using BrasilApiIntegration.Model.Response;

namespace BrasilApiIntegration.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
    }
}
