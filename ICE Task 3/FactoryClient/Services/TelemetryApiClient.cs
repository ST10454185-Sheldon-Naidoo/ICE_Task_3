using FactoryClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FactoryClient.Services
{
    public class TelemetryApiClient
    {
        // HttpClient instance that handles the API requests (Point 11)
        private readonly HttpClient _httpClient;

        public TelemetryApiClient(string baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public async Task<ApiResponseDto?> GetTelemetryDataAsync()
        {
            var response = await _httpClient.GetAsync("/api/telemetry");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<ApiResponseDto>(json, options);
        }
    }
}
