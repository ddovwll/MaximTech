using MaximTech.Application.Contracts;
using MaximTech.Application.Services;
using MaximTech.Domain.Contracts;
using MaximTech.Domain.Models;
using MaximTech.Infrastructure.Services.Sort;
using MaximTech.Tests.Mocks;

namespace MaximTech.Tests.Test;

public abstract class TestBase
{
    protected Lazy<StringProcessService> StringProcessService { get; }
    protected Lazy<ISortFactory> SortFactory { get; }
    protected Lazy<Settings> Settings { get; }
    protected Lazy<IRandomNumberGenerator> RandomNumberGenerator { get; }

    protected TestBase()
    {
        RandomNumberGenerator = new Lazy<IRandomNumberGenerator>(() => new RandomNumberGeneratorMock());
        SortFactory = new Lazy<ISortFactory>(() => new SortFactory(new List<ISort> { new QuickSort(), new TreeSort() }));
        Settings = new Lazy<Settings>(() => new Settings
        {
            BlackList = new List<string> { "abc", "ab", "edf" },
            ParallelLimit = 0
        });
        StringProcessService = new Lazy<StringProcessService>(() => new StringProcessService(RandomNumberGenerator.Value, SortFactory.Value, Settings.Value));
    }
}