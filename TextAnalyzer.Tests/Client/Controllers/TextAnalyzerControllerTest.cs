using Microsoft.AspNetCore.Mvc;
using Moq;
using TextAnalyzer.Application.Interfaces;
using TextAnalyzer.Application.Models;
using TextAnalyzer.Client.Controllers;
using TextAnalyzer.Client.Models;

namespace TextAnalyzer.Tests.Client.Controllers;

public class TextAnalyzerControllerTest
{
    private readonly Mock<ITextAnalyzerService> _mockService = new();
    private readonly TextAnalyzerController _controller;

    public TextAnalyzerControllerTest()
    {
        _controller = new TextAnalyzerController(_mockService.Object);

        _mockService.Setup(s => s.ProcessText(It.IsAny<string>())).Returns(new TextAnalyzerResult([new LetterData('a', 1, 100)]));
    }

    [Fact]
    public void AnalyzeText_Returns400_WhenRequestIsNull()
    {
        var result = _controller.AnalyzeText(null!);

        Assert.IsType<BadRequestResult>(result.Result);
        _mockService.Verify(s => s.ProcessText(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void AnalyzeText_Returns400_WhenRequestInputIsNull()
    {
        var result = _controller.AnalyzeText(new TextAnalyzerRequest(null!));

        Assert.IsType<BadRequestResult>(result.Result);
        _mockService.Verify(s => s.ProcessText(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void AnalyzeText_Returns200WithResult_WhenRequestIsValid()
    {
        var expected = new TextAnalyzerResponse([new LetterData('a', 1, 100)]);

        var result = _controller.AnalyzeText(new TextAnalyzerRequest("a"));

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var actual = Assert.IsType<TextAnalyzerResponse>(ok.Value);
        Assert.Equal(expected.LetterData, actual.LetterData);
    }

    [Fact]
    public void AnalyzeText_PassesRequestTextToService()
    {
        var result = _controller.AnalyzeText(new TextAnalyzerRequest("a"));

        _mockService.Verify(s => s.ProcessText("a"), Times.Once);
    }

}
