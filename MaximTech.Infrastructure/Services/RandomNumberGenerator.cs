using System.Text.Json;
using MaximTech.Application.Contracts;

namespace MaximTech.Infrastructure.Services;

public class RandomNumberGenerator : IRandomNumberGenerator
{
    private readonly HttpClient _httpClient;

    public RandomNumberGenerator(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<int> GetRandomNumberAsync(int max)
    {
        var response = await _httpClient.GetAsync($"?min=0&max={max}&count=1");
        if (!response.IsSuccessStatusCode)
        {
            return GenerateBuiltInRandom(max);
        }

        var content = await response.Content.ReadAsStreamAsync();
        var number = await JsonSerializer.DeserializeAsync<int[]>(content);
        return number is null || number.Length != 1 ? GenerateBuiltInRandom(max) : number[0];
    }

    private static int GenerateBuiltInRandom(int max)
    {
        var random = new Random();
        return random.Next(0, max);
    }
}