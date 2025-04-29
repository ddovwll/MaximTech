using ConsoleApps.Tasks;

var input = Console.ReadLine()!;
var result = StringReverse.Reverse(input, SortType.TreeSort);
Console.WriteLine(result);