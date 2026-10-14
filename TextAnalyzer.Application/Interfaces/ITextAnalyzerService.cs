using TextAnalyzer.Application.Models;

namespace TextAnalyzer.Application.Interfaces;

public interface ITextAnalyzerService
{
	public TextAnalyzerResponse ProcessText(string textInput);

}
