using System.Globalization;
using Blog.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blog.Application.Common.Services;
public class GeocodingService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IGeocodingService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly string _baseUrl = "https://maps.googleapis.com/maps/api/geocode/json?";
    private readonly string _apiKey = configuration["GeocodingApiKey"] ?? throw new Exception("Missing Geocoding Api Key");

    public async Task<string> GetAddressAsync(double latitude, double longitude)
    {
        var response = await _httpClient.GetAsync(_baseUrl + $"latlng={latitude.ToString(new CultureInfo("en-US"))},{longitude.ToString(new CultureInfo("en-US"))}&key={_apiKey}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        string content = await response.Content.ReadAsStringAsync();

        var json = (JObject)JsonConvert.DeserializeObject(content)!;
        return json.SelectToken("results[0].formatted_address")!.Value<string>()!;
    }
}
