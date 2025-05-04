using MaximTech.Application.Contracts;

namespace MaximTech.Tests.Mocks;

public class RandomNumberGeneratorMock : IRandomNumberGenerator
{
    public int GeneratedNumber { get; private set; }
    
    public Task<int> GetRandomNumberAsync(int max)
    {
        var random = new Random();
        GeneratedNumber = random.Next(max);
        return Task.FromResult(GeneratedNumber);
    }
}