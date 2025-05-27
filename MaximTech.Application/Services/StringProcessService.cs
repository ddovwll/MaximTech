using System.Text;
using System.Text.RegularExpressions;
using MaximTech.Application.Contracts;
using MaximTech.Domain.Contracts;
using MaximTech.Domain.Models;

namespace MaximTech.Application.Services;

public class StringProcessService
{
    private static readonly Regex LatinLowerCase = new("^[a-z]+$");
    private static readonly Regex InappropriateSymbols = new("[^a-z]");
    private static readonly Regex SubstringBand = new("[aeiouy].*[aeiouy]");

    private readonly IRandomNumberGenerator _randomNumberGenerator;
    private readonly ISortFactory _sortFactory;
    private readonly Settings _settings;

    public StringProcessService(
        IRandomNumberGenerator randomNumberGenerator,
        ISortFactory sortFactory,
        Settings settings
    )
    {
        _randomNumberGenerator = randomNumberGenerator;
        _sortFactory = sortFactory;
        _settings = settings;
    }

    public async Task<(string reversed, string repetitions, string substringInBand, string sorted, string trimmed)>
        ProcessString(
            string input,
            SortType sortType
        )
    {
        if (_settings.BlackList.Contains(input))
        {
            throw new ArgumentException($"Строка: {input} находится в черном списке");
        }
        
        if (!LatinLowerCase.IsMatch(input))
        {
            var matches = InappropriateSymbols.Matches(input);
            var inappropriateSymbols = string.Join(", ", matches.Select(m => m.Value).ToHashSet());
            throw new ArgumentException($"Введены неподходящие символы: {inappropriateSymbols}");
        }

        var reversed = ProcessReverse(input);
        var repetitions = Repetitions(reversed);
        var substringInBand = SubstringInBand(reversed);
        var chars = reversed.ToCharArray();
        Sort(chars, sortType);
        var trimmed = await Trim(reversed);
        return (reversed, repetitions, substringInBand, string.Concat(chars), trimmed);
    }

    private static string ProcessReverse(string input)
    {
        if (input.Length % 2 != 0)
        {
            return string.Concat(string.Concat(input.Reverse().ToArray()), input);
        }

        var firstSubstring = input[..(input.Length / 2)];
        var secondSubstring = input[(input.Length / 2)..];
        return string.Concat(string.Concat(firstSubstring.Reverse().ToArray()),
            string.Concat(secondSubstring.Reverse().ToArray()));
    }

    private static string Repetitions(string input)
    {
        var charsCount = input
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, y => y.Count());

        var stringBuilder = new StringBuilder();

        foreach (var group in charsCount)
        {
            stringBuilder.Append(group.Key);
            stringBuilder.Append(": ");
            stringBuilder.Append(group.Value);
            stringBuilder.Append("; ");
        }

        return stringBuilder.ToString();
    }

    private static string SubstringInBand(string input)
    {
        var matches = SubstringBand.Matches(input);
        return string.Concat(matches.Select(m => m.Value));
    }

    private void Sort(char[] input, SortType sortType)
    {
        var sortAlgorithm = _sortFactory.GetSortAlgorithm(sortType);
        sortAlgorithm.Sort(input);
    }

    private async Task<string> Trim(string input)
    {
        var removementIndex = await _randomNumberGenerator.GetRandomNumberAsync(input.Length);
        return input.Remove(removementIndex, 1);
    }
}