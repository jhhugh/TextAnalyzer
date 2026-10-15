using Microsoft.AspNetCore.Mvc;
using Moq;
using TextAnalyzer.Application.Interfaces;
using TextAnalyzer.Client.Controllers;

namespace TextAnalyzer.Tests.Client.Controllers;

public class TextAnalyzerControllerTest
{
    private readonly Mock<ITextAnalyzerService> _mockService = new();
    private readonly TextAnalyzerController _controller;

    public TextAnalyzerControllerTest()
    {
        _controller = new TextAnalyzerController(_mockService.Object);
    }

    [Fact]
    public void AnalyzeText_Returns400_WhenInputIsNull()
    {
        var result = _controller.AnalyzeText(null!);

        Assert.IsType<BadRequestResult>(result.Result);
        _mockService.Verify(s => s.ProcessText(It.IsAny<string>()), Times.Never);
    }
}
