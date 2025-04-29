using System.Text.Json;

namespace ConsoleApps.Tasks.Infrastructure;

public static class RandomNumberGenerator
{
    private static readonly HttpClient HttpClient;

    static RandomNumberGenerator()
    {
        HttpClient = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        })
        {
            BaseAddress = new Uri("https://www.randomnumberapi.com/api/v1.0/random")
        };
    }
    
    public static async Task<int> GetRandomNumberAsync(int max)
    {
        var response = await HttpClient.GetAsync($"?min=0&max={max}&count=1");
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