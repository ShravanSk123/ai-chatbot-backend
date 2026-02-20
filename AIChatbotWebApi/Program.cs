var builder = WebApplication.CreateBuilder(args);

// Bind to Northflank port
builder.WebHost.UseUrls($"http://0.0.0.0:{Environment.GetEnvironmentVariable("PORT")}");

// Config
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<GroqSettings>(
    builder.Configuration.GetSection("GroqCloud"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",
                "https://ai-chatbot-frontend-theta.vercel.app"
              )
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseCors("Production");
app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", () => "OK");

app.Run();

public class GroqSettings
{
    public string? ApiKey { get; set; }
    public string? Model { get; set; }
    public string? ChatUrl { get; set; }
}
