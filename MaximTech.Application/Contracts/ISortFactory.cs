using MaximTech.Domain.Contracts;
using MaximTech.Domain.Models;

namespace MaximTech.Application.Contracts;

public interface ISortFactory
{
    ISort GetSortAlgorithm(SortType type);
}