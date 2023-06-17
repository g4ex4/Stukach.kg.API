using Application.Integrations.Geocoding.Models;
using Domain.Dto;
using RestSharp;

namespace Application.Integrations.Geocoding;

public class Geocoding
{
    private readonly string _apiKey;

    public Geocoding(string apiKey)
    {
        _apiKey = apiKey;
    }

    public GeoCodeResponse Geocode(CoordinateData data)
    {
        var client = new RestClient("https://api.opencagedata.com/geocode/v1/json");
        var request = new RestRequest();
        request.AddParameter("key", _apiKey);
        request.AddParameter("q", $"{data.Latitude}+{data.Longitude}");

        var response = client.Execute<GeoCodeResponse>(request);
        return response.Data;
    }
}