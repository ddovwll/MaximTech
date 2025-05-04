using MaximTech.Domain.Contracts;
using MaximTech.Domain.Models;

namespace MaximTech.Tests.Test;

public class SortTests : TestBase
{
    private ISort _treeSort;
    private ISort _quickSort;

    [SetUp]
    public void Setup()
    {
        var sortFactory = SortFactory.Value;
        _treeSort = sortFactory.GetSortAlgorithm(SortType.TreeSort);
        _quickSort = sortFactory.GetSortAlgorithm(SortType.QuickSort);
    }

    [Test]
    [TestCaseSource(nameof(AddTestCases))]
    public void TreeSort_SortsAnArray(char[] input, char[] expected)
    {
        // Act
        _treeSort.Sort(input);
        
        // Assert
        Assert.That(input, Is.EqualTo(expected));
    }
    
    [Test]
    [TestCaseSource(nameof(AddTestCases))]
    public void QuickSort_SortsAnArray(char[] input, char[] expected)
    {
        // Act
        _quickSort.Sort(input);
        
        // Assert
        Assert.That(input, Is.EqualTo(expected));
    }

    private static IEnumerable<TestCaseData> AddTestCases()
    {
        yield return new TestCaseData(
            new [] { 'c', 'a', 'b' },
            new [] { 'a', 'b', 'c' }
        ).SetName("Unsorted small array");

        yield return new TestCaseData(
            new [] { 'z', 'x', 'y' },
            new [] { 'x', 'y', 'z' }
        ).SetName("Another unsorted array");

        yield return new TestCaseData(
            new [] { 'a', 'a', 'b', 'b', 'c' },
            new [] { 'a', 'a', 'b', 'b', 'c' }
        ).SetName("Already sorted with duplicates");

        yield return new TestCaseData(
            new [] { 'b', 'B', 'a', 'A' },
            new [] { 'A', 'B', 'a', 'b' }
        ).SetName("Uppercase and lowercase letters");

        yield return new TestCaseData(
            Array.Empty<char>(),
            Array.Empty<char>()
        ).SetName("Empty array");

        yield return new TestCaseData(
            new [] { 'd' },
            new [] { 'd' }
        ).SetName("Single element array");

        yield return new TestCaseData(
            new [] { '!', 'z', '#', 'a' },
            new [] { '!', '#', 'a', 'z' }
        ).SetName("Special characters and letters");

        yield return new TestCaseData(
            new [] { '1', '3', '2', '0' },
            new [] { '0', '1', '2', '3' }
        ).SetName("Numeric characters");
    }
}