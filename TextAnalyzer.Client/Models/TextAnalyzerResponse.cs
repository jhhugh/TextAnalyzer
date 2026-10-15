using TextAnalyzer.Application.Models;

namespace TextAnalyzer.Client.Models;

public sealed record TextAnalyzerResponse(List<LetterData> LetterData);
