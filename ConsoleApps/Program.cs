using ConsoleApps.Tasks;

var input = Console.ReadLine()!;
var result = await StringReverse.Reverse(input, SortType.TreeSort);
Console.WriteLine(result);