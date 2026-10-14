
using TextAnalyzer.Application.Models;
using TextAnalyzer.Application.Services;

namespace TextAnalyzer.Tests.Application.Services;

public class TextAnalyzerServiceTest
{

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
        //Arrange
        var textAnalyzerService = new TextAnalyzerService();


        //Act
        var textAnalyzerResponse = textAnalyzerService.ProcessText(textInput);

        //Assert
        Assert.Empty(textAnalyzerResponse.LetterData);

    }


    [Theory]
    [MemberData(nameof(MockLetterDataWithNumbersAndSpecialCharacters))]
    public void ProcessText_IgnoresSpecialCharaterAndNumbers(string textInput, LetterData[] letterData)
    {
        //Arrange
        var textAnalyzerService = new TextAnalyzerService();

        //Act
        var textAnalyzerResponse = textAnalyzerService.ProcessText(textInput);


        //Assert

        foreach (var letter in letterData)
        {
            var found = Assert.Single(textAnalyzerResponse.LetterData, l => l.Letter == letter.Letter);
            Assert.Equal(letter.LetterCount, found.LetterCount);
            Assert.Equal(letter.LetterPercentage, found.LetterPercentage, 2);
        }

    }

    [Theory]
    [MemberData(nameof(MockLetterData))]
    public void ProcessText_ReturnsCorrectLetterCountAndPercentage_BasedOnInputSize(string textInput, LetterData[] letterData)
    {
        //Arrange
        var textAnalyzerService = new TextAnalyzerService();

        //Act
        var textAnalyzerResponse = textAnalyzerService.ProcessText(textInput);


        //Assert

        foreach (var letter in letterData)
        {
            var found = Assert.Single(textAnalyzerResponse.LetterData, l => l.Letter == letter.Letter);
            Assert.Equal(letter.LetterCount, found.LetterCount);
            Assert.Equal(letter.LetterPercentage, found.LetterPercentage, 2);
        }

    }

    [Fact]
    public void ProcessText_IgnoresCase()
    {
        //Arrange
        var textAnalyzerService = new TextAnalyzerService();

        //Act
        var textAnalyzerResponse = textAnalyzerService.ProcessText("aA");


        //Assert

        var letter = Assert.Single(textAnalyzerResponse.LetterData);
        Assert.Equal('a', letter.Letter);
        Assert.Equal(2, letter.LetterCount);
        Assert.Equal(100, letter.LetterPercentage);
    }

    [Fact]
    public void ProcessText_IgnoresWhiteSpace()
    {
        //Arrange
        var textAnalyzerService = new TextAnalyzerService();

        //Act
        var textAnalyzerResponse = textAnalyzerService.ProcessText("a  ");


        //Assert

        var letter = Assert.Single(textAnalyzerResponse.LetterData);
        Assert.Equal('a', letter.Letter);
        Assert.Equal(1, letter.LetterCount);
        Assert.Equal(100, letter.LetterPercentage);
    }

    [Fact]
    public void ProcessText_AddsUpTo100Percent()
    {
        //Arrange
        var textAnalyzerService = new TextAnalyzerService();

        //Act
        var textAnalyzerResponse = textAnalyzerService.ProcessText("abc");


        //Assert
        Assert.Equal(100, textAnalyzerResponse.LetterData.Sum(x => x.LetterPercentage), 2);
    }


}

