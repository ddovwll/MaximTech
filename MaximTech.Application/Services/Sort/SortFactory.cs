using MaximTech.Domain.Contracts;
using MaximTech.Domain.Models;

namespace MaximTech.Application.Services.Sort;

public class SortFactory : ISortFactory
{
    private readonly IEnumerable<ISort> _sortAlgorithms;

    public SortFactory(IEnumerable<ISort> sortAlgorithms)
    {
        _sortAlgorithms = sortAlgorithms;
    }
    
    public ISort GetSortAlgorithm(SortType sortType)
    {
        return sortType switch
        {
            SortType.QuickSort => _sortAlgorithms.OfType<QuickSort>().First(),
            SortType.TreeSort => _sortAlgorithms.OfType<TreeSort>().First(),
            _ => throw new ArgumentException("Неизвестный тип сортировки", nameof(sortType))
        };
    }
}