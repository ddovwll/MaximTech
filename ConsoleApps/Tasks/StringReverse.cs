using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApps.Tasks;

public static class StringReverse
{
    private static readonly Regex LatinLowerCase = new("^[a-z]+$");
    private static readonly Regex InappropriateSymbols = new("[^a-z]");

    public static (string reversed, string repetitions) Reverse(string input)
    {
        if (!LatinLowerCase.IsMatch(input))
        {
            var matches = InappropriateSymbols.Matches(input);
            var inappropriateSymbols = string.Join(", ", matches.Select(m => m.Value).ToHashSet());
            throw new ArgumentException($"Введены неподходящие символы: {inappropriateSymbols}");
        }

        var reversed = ProcessReverse(input);
        var repetitions = Repetitions(reversed);
        return (reversed, repetitions);
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
}