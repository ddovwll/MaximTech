using System.Text.RegularExpressions;
using MaximTech.Application.Services;
using MaximTech.Domain.Models;
using MaximTech.Tests.Mocks;

namespace MaximTech.Tests.Test;

public class StringProcessorTest : TestBase
{
    private static readonly Regex LatinLowerCase = new("^[a-z]+$");
    private Settings _settings;
    private StringProcessService _stringProcessService;
    private RandomNumberGeneratorMock _randomNumberGenerator;

    [SetUp]
    public void Setup()
    {
        _settings = Settings.Value;
        _stringProcessService = StringProcessService.Value;
        _randomNumberGenerator = (RandomNumberGeneratorMock)RandomNumberGenerator.Value;
    }

    [Test]
    [TestCaseSource(nameof(AddTestCases))]
    public async Task Reverse_ProcessesReverse(string input,
        (string reversed, string repetitions, string substringInBand, string sorted) expected
    )
    {
        // Assert Exception
        if (_settings.BlackList.Contains(input) || !LatinLowerCase.IsMatch(input))
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _stringProcessService.ProcessString(input, SortType.QuickSort);
            });
            
            return;
        }

        // Act
        var result = await _stringProcessService.ProcessString(input, SortType.QuickSort);
        var generatedNumber = _randomNumberGenerator.GeneratedNumber;
        var trimmed = result.reversed.Remove(generatedNumber, 1);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.reversed, Is.EqualTo(expected.reversed));
            Assert.That(result.repetitions, Is.EqualTo(expected.repetitions));
            Assert.That(result.sorted, Is.EqualTo(expected.sorted));
            Assert.That(result.substringInBand, Is.EqualTo(expected.substringInBand));
            Assert.That(result.trimmed, Is.EqualTo(trimmed));
        });
    }

    private static IEnumerable<TestCaseData> AddTestCases()
    {
        yield return new TestCaseData(
            "abcd",
            ("badc", "b: 1; a: 1; d: 1; c: 1; ", "", "abcd")
        ).SetName("Even length - simple case");

        yield return new TestCaseData(
            "MaximTech",
            null
        ).SetName("Even length - contains inappropriate symbols");

        yield return new TestCaseData(
            "maximtech",
            ("hcetmixammaximtech", "h: 2; c: 2; e: 2; t: 2; m: 4; i: 2; x: 2; a: 2; ", "etmixammaximte",
                "aacceehhiimmmmttxx")
        ).SetName("Even length - empty string");

        yield return new TestCaseData(
            "aa",
            ("aa", "a: 2; ", "aa", "aa")
        ).SetName("Even length - identical characters");

        yield return new TestCaseData(
            "abc",
            null
        ).SetName("Odd length - simple case");

        yield return new TestCaseData(
            "hello",
            ("ollehhello", "o: 2; l: 4; e: 2; h: 2; ", "ollehhello", "eehhlllloo")
        ).SetName("Odd length - longer word");

        yield return new TestCaseData(
            "x",
            ("xx", "x: 2; ", "", "xx")
        ).SetName("Odd length - single character");
    }
}