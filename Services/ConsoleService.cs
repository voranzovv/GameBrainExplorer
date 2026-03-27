using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GameBrainExplorer.Models;

namespace GameBrainExplorer.Services
{
    public class ConsoleService
    {
        private readonly HttpClient _client;

        public ConsoleService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<GameConsole>> GetAllConsoles()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.gamebrain.co/v1/games?query=strategy+games");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", "c09964815afd463c82a6d86ce01d5c5d");

            using var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<GameApiResponse>(body, options);

            if (data?.Results != null)
            {
                foreach (var game in data.Results)
                {
                    string yearText = game.Year.HasValue ? game.Year.Value.ToString() : "Unknown";
                    Console.WriteLine($"Name: {game.Name}, Year: {yearText}, Genre: {game.Genre}");
                }
            }

            return data?.Results ?? new List<GameConsole>();
        }
    }
}