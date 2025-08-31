using Services;
using Middleware;

var builder = WebApplication.CreateBuilder(args);

// Services configuration
builder.Services.AddControllers();
builder.Services.AddHttpClient<OpenAIService>();
builder.Services.AddScoped<OpenAIService>();

// API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "AI API", Version = "v1" });
});

// CORS configuration
builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        // Development: Esnek politika - tüm kaynaklardan erişim
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    }
    else
    {
        // Production: Kısıtlı politika - belirli domain'ler eklenecek
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy.WithMethods("GET", "POST", "PUT", "DELETE")
                  .WithHeaders("Content-Type", "Authorization")
                  .AllowCredentials();
            // TODO: Production domain'leri eklenecek
            // .WithOrigins("https://yourdomain.com", "https://www.yourdomain.com")
        });
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AI API v1");
        c.RoutePrefix = string.Empty; // Swagger UI ana sayfada açılır
    });
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();

