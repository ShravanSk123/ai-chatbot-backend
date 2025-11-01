namespace AIChatbotWebApi.Models;

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
