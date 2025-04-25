namespace ConsoleApps.Tasks;

public static class StringReverse
{
    public static string Reverse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        if (input.Length % 2 != 0)
        {
            return string.Concat(new string(input.Reverse().ToArray()), input);
        }
        
        var firstSubstring = input[..(input.Length / 2)];
        var secondSubstring = input[(input.Length / 2)..];
        return string.Concat(new string(firstSubstring.Reverse().ToArray()),
            new string(secondSubstring.Reverse().ToArray()));
    }
}