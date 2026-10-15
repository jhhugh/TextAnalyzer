using TextAnalyzer.Application.Models;

namespace TextAnalyzer.Application.Interfaces;

public interface ITextAnalyzerService
{
	public TextAnalyzerResult ProcessText(string textInput);

}
