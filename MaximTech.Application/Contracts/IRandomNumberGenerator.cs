namespace MaximTech.Application.Contracts;

public interface IRandomNumberGenerator
{
    Task<int> GetRandomNumberAsync(int max);
}