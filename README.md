# AI API

OpenAI API entegrasyonu ile ASP.NET Core Web API projesi.

## Kurulum

1. Projeyi klonlayın
2. API anahtarını User Secrets ile ayarlayın:
   ```bash
   dotnet user-secrets set "OpenAI:ApiKey" "your-openai-api-key"
   ```
3. Projeyi çalıştırın:
   ```bash
   dotnet run
   ```

## Güvenlik

- API anahtarları User Secrets ile güvenli şekilde saklanır
- Hassas bilgiler kaynak kodda yer almaz
- Production ortamında Environment Variables kullanın

## API Endpoints

- `POST /api/openai/chat` - OpenAI chat completion

## Gereksinimler

- .NET 9.0
- OpenAI API anahtarı

## Geliştirme

Swagger UI'ya erişim için: `https://localhost:5001/swagger`
