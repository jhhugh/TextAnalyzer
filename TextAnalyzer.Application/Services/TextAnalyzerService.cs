using TextAnalyzer.Application.Interfaces;
using TextAnalyzer.Application.Models;

namespace TextAnalyzer.Application.Services;

public class TextAnalyzerService : ITextAnalyzerService
{
	public TextAnalyzerResult ProcessText(string textInput)
	{
		var filteredInput = textInput.ToLowerInvariant().Where(char.IsLetter).ToList();

		if (filteredInput.Count == 0)
		{
			return new TextAnalyzerResult([]);
		}

		var letterData = filteredInput
		.GroupBy(x => x)
		.Select(c => new LetterData(c.Key, c.Count(), (decimal)c.Count() / filteredInput.Count * 100))
		.ToList();

		return new TextAnalyzerResult(letterData);

	}
}
