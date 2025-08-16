using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AIChatbotWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ChatController> _logger;
    private readonly GroqSettings _settings;

    public ChatController(HttpClient httpClient, IOptions<GroqSettings> settings, ILogger<ChatController> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings.Value;
    }

    [HttpPost]
    public async Task<IActionResult> PostMessage([FromBody] ChatRequest request)
    {
        var payload = new
        {
            model = _settings.Model,
            messages = new[]
                {
                    new { role = "user", content = request.Message }
                }
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

        var response = await _httpClient.PostAsync(_settings.ChatUrl, content);
        var resultString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var groqResponse = JsonSerializer.Deserialize<GroqResponse>(resultString);
                var assistantReply = groqResponse?.choices?[0]?.message?.content ?? "No response";
                var cleanReply = CleanMarkdown(assistantReply);
                return Ok(new { Response = cleanReply });
            }
            catch
            {
                // Fallback: return full response if parsing fails
                return Ok(new { Response = resultString });
            }
        }
        else
        {
            return StatusCode((int)response.StatusCode, new { Error = resultString });
        }
    }
    private string CleanMarkdown(string text)
    {
        var plainText = Regex.Replace(text, @"^#+\s*", "", RegexOptions.Multiline);
        plainText = Regex.Replace(plainText, @"(\*\*|\*|__|_)", "");
        plainText = Regex.Replace(plainText, @"```.*?```", "", RegexOptions.Singleline);
        plainText = Regex.Replace(plainText, @"^-{3,}", "", RegexOptions.Multiline);
        plainText = Regex.Replace(plainText, @"^\|.*\|$", "", RegexOptions.Multiline);
        plainText = Regex.Replace(plainText, @"\n{2,}", "\n").Trim();
        return plainText;
    }
}

public class ChatRequest
{
    public string? Message { get; set; }
}
public class GroqResponse
{
    public Choice[]? choices { get; set; }
}

public class Choice
{
    public Message? message { get; set; }
}

public class Message
{
    public string? role { get; set; }
    public string? content { get; set; }
}
