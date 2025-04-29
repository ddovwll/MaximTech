using System.Text;
using System.Text.RegularExpressions;
using ConsoleApps.Tasks.Helpers;
using ConsoleApps.Tasks.Infrastructure;

namespace ConsoleApps.Tasks;

public static class StringReverse
{
    private static readonly Regex LatinLowerCase = new("^[a-z]+$");
    private static readonly Regex InappropriateSymbols = new("[^a-z]");
    private static readonly Regex SubstringBand = new("[aeiouy].*[aeiouy]");

    public static async Task<(string reversed, string repetitions, string substringInBand, string sorted, string trimmed)>
        Reverse(
            string input,
            SortType sortType
        )
    {
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

    private static void Sort(char[] input, SortType sortType)
    {
        switch (sortType)
        {
            case SortType.QuickSort:
                QuickSort.Sort(input, 0, input.Length - 1);
                break;
            case SortType.TreeSort:
                TreeSort.Sort(input);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sortType), sortType, null);
        }
    }

    private static async Task<string> Trim(string input)
    {
        var removementIndex = await RandomNumberGenerator.GetRandomNumberAsync(input.Length);
        return input.Remove(removementIndex, 1);
    }
}

public enum SortType
{
    QuickSort,
    TreeSort
}