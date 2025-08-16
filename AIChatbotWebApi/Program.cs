var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

//cors
builder.Services.AddCors(options =>
{
    // More restrictive policy for production
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public class GroqSettings
{
    public string? ApiKey { get; set; }
    public string? Model { get; set; }
    public string? ChatUrl { get; set; }
}
