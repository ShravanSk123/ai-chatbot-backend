using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AIChatbotWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string hfToken = "YOUR_HF_API_TOKEN";

    public ChatController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost]
    public async Task<IActionResult> PostMessage([FromBody] ChatRequest request)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", hfToken);

        var payload = new { inputs = request.Message };
        var jsonPayload = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api-inference.huggingface.co/models/facebook/blenderbot-400M-distill", jsonPayload);
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }
}

public class ChatRequest
{
    public string? Message { get; set; }
}
