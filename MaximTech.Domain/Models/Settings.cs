namespace MaximTech.Domain.Models;

public class Settings
{
    public List<string> BlackList { get; init; } = new();
    public int ParallelLimit {get; init;}
}