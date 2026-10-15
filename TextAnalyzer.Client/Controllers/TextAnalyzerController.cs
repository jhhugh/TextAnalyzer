using Microsoft.AspNetCore.Mvc;
using TextAnalyzer.Application.Interfaces;
using TextAnalyzer.Client.Models;

namespace TextAnalyzer.Client.Controllers
{
    [ApiController]
    [Route("api/analysis")]
    public class TextAnalyzerController(ITextAnalyzerService textAnalyzerService) : ControllerBase
    {
        private readonly ITextAnalyzerService _textAnalyzerService = textAnalyzerService;

        [HttpPost]
        public ActionResult<TextAnalyzerResponse> AnalyzeText(TextAnalyzerRequest request)
        {
            if(request is null || request.TextInput is null)
            {
                return BadRequest();
            }
            return Ok(TextAnalyzerResponse.From(_textAnalyzerService.ProcessText(request.TextInput)));
        }

    }
}
