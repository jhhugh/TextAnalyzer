using TextAnalyzer.Application.Models;

namespace TextAnalyzer.Client.Models;

public sealed record TextAnalyzerResponse(List<LetterData> LetterData)
{
	public static TextAnalyzerResponse From(TextAnalyzerResult result) => new(result.LetterData);
}
