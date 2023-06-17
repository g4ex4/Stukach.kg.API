using System.Text.Json;
using Application.Integrations.Geocoding.Models;

namespace Application.Integrations.Geocoding;

public class OpenCageApiClient
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public OpenCageApiClient(string apiKey)
    {
        _apiKey = apiKey;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.opencagedata.com/geocode/v1/");
    }

    public async Task<GeoCodeResponse> Geocode(string query)
    {
        var url = $"json?q={Uri.EscapeDataString(query)}&key={_apiKey}";

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GeoCodeResponse>(json);
            return result;
        }

        throw new Exception($"Ошибка при выполнении запроса к OpenCage Geocoding API: {response.StatusCode}");
    }
}