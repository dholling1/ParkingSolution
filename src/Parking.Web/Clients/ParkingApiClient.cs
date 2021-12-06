using System;
using Parking.Core.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Parking.Web.Clients
{
    public class ParkingApiClient : IParkingApiClient
    {
        private HttpClient _client;
        private JsonSerializerOptions _jsonOptions;

        public ParkingApiClient(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("ParkingApiClient");
            _jsonOptions = new JsonSerializerOptions
                {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        }

        public async Task<IEnumerable<ParkingLot>> GetParkingLots()
        {
            var data = await _client.GetFromJsonAsync<List<ParkingLot>>($"Parking");
            return data;
        }

        public async Task<ParkingLot> GetParkingLot(int id)
        {
            var data = await _client.GetFromJsonAsync<ParkingLot>($"Parking/{id}");
            return data;
        }

        public async Task<int> IncrementParkingLot(int id)
        {
            var result = await _client.PutAsJsonAsync($"Parking/{id}/increment", "", _jsonOptions);
            var data = await ReadIntFromResult(result, _jsonOptions);
            return data;
        }

        public async Task<int> DecrementParkingLot(int id)
        {
            var result = await _client.PutAsJsonAsync($"Parking/{id}/decrement", "", _jsonOptions);
            var data = await ReadIntFromResult(result, _jsonOptions);
            return data;
        }

        private static async Task<int> ReadIntFromResult(HttpResponseMessage response, JsonSerializerOptions options)
        {
            var msg = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<int>(msg, options);
            }
            else
            {
                throw new Exception("Error reading response message");
            }
        }
    }
}
