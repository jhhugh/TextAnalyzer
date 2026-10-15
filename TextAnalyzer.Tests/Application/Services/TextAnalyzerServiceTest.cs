
using TextAnalyzer.Application.Models;
using TextAnalyzer.Application.Services;

namespace TextAnalyzer.Tests.Application.Services;

public class TextAnalyzerServiceTest
{
    private readonly TextAnalyzerService _service;

    public TextAnalyzerServiceTest()
    {
        _service = new TextAnalyzerService();
    }

    public static TheoryData<string, LetterData[]> MockLetterData => new()
    {
        { "abc", new[] { new LetterData('a', 1, 33.33m), new LetterData('b', 1, 33.33m), new LetterData('c', 1, 33.33m) } },
        { "aabb",   new[] { new LetterData('a', 2, 50), new LetterData('b', 2, 50) } },
        { "aab",   new[] { new LetterData('a', 2, 66.67m), new LetterData('b', 1, 33.33m) } },
        { "a",   new[] { new LetterData('a', 1, 100) } },
    };

    public static TheoryData<string, LetterData[]> MockLetterDataWithNumbersAndSpecialCharacters => new()
    {
        { "ab23", new[] { new LetterData('a', 1, 50), new LetterData('b', 1, 50) } },
        { "a!cc",   new[] { new LetterData('a', 1, 33.33m), new LetterData('c', 2, 66.67m) } },
        { "a1!",   new[] { new LetterData('a', 1, 100) } },
    };


    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("12  ")]
    [InlineData("#@1")]
    public void ProcessText_ReturnsEmptyResult_WhenInputIsNotALetter(string textInput)
    {
        var result = _service.ProcessText(textInput);

        Assert.Empty(result.LetterData);

    }


    [Theory]
    [MemberData(nameof(MockLetterDataWithNumbersAndSpecialCharacters))]
    public void ProcessText_IgnoresSpecialCharaterAndNumbers(string textInput, LetterData[] letterData)
    {
        var result = _service.ProcessText(textInput);

        foreach (var letter in letterData)
        {
            var found = Assert.Single(result.LetterData, l => l.Letter == letter.Letter);
            Assert.Equal(letter.LetterCount, found.LetterCount);
            Assert.Equal(letter.LetterPercentage, found.LetterPercentage, 2);
        }

    }

    [Theory]
    [MemberData(nameof(MockLetterData))]
    public void ProcessText_ReturnsCorrectLetterCountAndPercentage_BasedOnInputSize(string textInput, LetterData[] letterData)
    {
        var result = _service.ProcessText(textInput);

        foreach (var letter in letterData)
        {
            var found = Assert.Single(result.LetterData, l => l.Letter == letter.Letter);
            Assert.Equal(letter.LetterCount, found.LetterCount);
            Assert.Equal(letter.LetterPercentage, found.LetterPercentage, 2);
        }

    }

    [Fact]
    public void ProcessText_IgnoresCase()
    {
        var result = _service.ProcessText("aA");

        var letter = Assert.Single(result.LetterData);
        Assert.Equal('a', letter.Letter);
        Assert.Equal(2, letter.LetterCount);
        Assert.Equal(100, letter.LetterPercentage);
    }

    [Fact]
    public void ProcessText_IgnoresWhiteSpace()
    {
        var result = _service.ProcessText("a  ");

        var letter = Assert.Single(result.LetterData);
        Assert.Equal('a', letter.Letter);
        Assert.Equal(1, letter.LetterCount);
        Assert.Equal(100, letter.LetterPercentage);
    }

    [Fact]
    public void ProcessText_AddsUpTo100Percent()
    {
        var result = _service.ProcessText("abc");

        Assert.Equal(100, result.LetterData.Sum(x => x.LetterPercentage), 2);
    }


}

