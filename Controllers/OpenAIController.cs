using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace AIAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly OpenAIService _openAIService;

        public OpenAIController(OpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost("test")]
        public async Task<IActionResult> TestPrompt([FromBody] OpenAIRequest request)
        {
            try
            {
                var result = await _openAIService.GetCompletionAsync(request.Prompt);
                return Ok(new { message = result });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "OpenAI API Error",
                    Detail = ex.Message,
                    Status = 400
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }
    }
}
