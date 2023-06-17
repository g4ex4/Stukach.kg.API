using Newtonsoft.Json;

namespace Application.Integrations.Geocoding.Models;

public class GeoCodeResponse
{
    [JsonProperty("results")]
    public Result Results { get; set; }
}

public class Result
{
    [JsonProperty("components")]
    public Component Components { get; set; }
}

public class Component
{
    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("hamlet")]
    public string Hamlet { get; set; }

    [JsonProperty("village")]
    public string Village { get; set; }
}