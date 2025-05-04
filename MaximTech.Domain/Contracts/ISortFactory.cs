using MaximTech.Domain.Models;

namespace MaximTech.Domain.Contracts;

public interface ISortFactory
{
    ISort GetSortAlgorithm(SortType type);
}