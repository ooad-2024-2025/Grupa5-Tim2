using Microsoft.Extensions.Options;

namespace OffroadAdventure.OpenRouteServiceAPI
{
    public class OpenRouteServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenRouteServiceClient(HttpClient httpClient, IOptions<ORSOptions> options)
        {
            _httpClient = httpClient;
            _apiKey = options.Value.ApiKey;

            _httpClient.BaseAddress = new Uri("https://api.openrouteservice.org/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", _apiKey);
        }

        public async Task<string> GetRutaAsync(double startLon, double startLat, double endLon, double endLat)
        {
            string url = $"v2/directions/driving-car?start={startLon.ToString(System.Globalization.CultureInfo.InvariantCulture)},{startLat.ToString(System.Globalization.CultureInfo.InvariantCulture)}&end={endLon.ToString(System.Globalization.CultureInfo.InvariantCulture)},{endLat.ToString(System.Globalization.CultureInfo.InvariantCulture)}&preference=shortest";


            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Greška u API pozivu: {response.StatusCode} → {error}");
            }

            return await response.Content.ReadAsStringAsync();
        }

    }
}
