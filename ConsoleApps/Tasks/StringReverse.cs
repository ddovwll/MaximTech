using System.Text.RegularExpressions;

namespace ConsoleApps.Tasks;

public static class StringReverse
{
    private static readonly Regex LatinLowerCase = new("^[a-z]+$");
    private static readonly Regex InappropriateSymbols = new("[^a-z]");

    public static string Reverse(string input)
    {
        if (!LatinLowerCase.IsMatch(input))
        {
            var matches = InappropriateSymbols.Matches(input);
            var inappropriateSymbols = string.Join(", ", matches.Select(m => m.Value).ToHashSet());
            throw new ArgumentException($"Введены неподходящие символы: {inappropriateSymbols}");
        }

        if (input.Length % 2 != 0)
        {
            return string.Concat(string.Concat(input.Reverse().ToArray()), input);
        }

        var firstSubstring = input[..(input.Length / 2)];
        var secondSubstring = input[(input.Length / 2)..];
        return string.Concat(string.Concat(firstSubstring.Reverse().ToArray()),
            string.Concat(secondSubstring.Reverse().ToArray()));
    }
}