using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Application.Integrations.Geocoding.Models;

public class GeoCodeResponse
{
    [JsonPropertyName("results")]
    public Result[] Results { get; set; }
}

public class Result
{
    [JsonPropertyName("components")]
    public Component Components { get; set; }
}

public class Component
{
    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("hamlet")]
    public string? Hamlet { get; set; }

    [JsonPropertyName("village")]
    public string? Village { get; set; }

    [JsonPropertyName("county")]
    public string? County { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }
}