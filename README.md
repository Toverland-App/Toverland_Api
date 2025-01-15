Hier is een voorbeeld van een README.md-bestand voor de Toverland_Api repository:

```markdown
# Toverland API

Dit is de API voor het Toverland attractiepark beheer systeem. Deze API biedt endpoints voor het beheren van attracties, onderhoud, gebieden, medewerkers en bezoekersaantallen.

## Inhoud

- [Installatie](#installatie)
- [Gebruik](#gebruik)
- [Endpoints](#endpoints)
- [Voorbeeldcode](#voorbeeldcode)
- [Bijdragen](#bijdragen)
- [Licentie](#licentie)

## Installatie

1. Clone de repository:
   ```bash
   git clone https://github.com/Toverland-App/Toverland_Api.git
   ```
2. Navigeer naar de projectmap:
   ```bash
   cd Toverland_Api
   ```
3. Installeer de benodigde dependencies:
   ```bash
   dotnet restore
   ```
4. Configureer de databaseverbinding in `appsettings.json`.

## Gebruik

Start de applicatie met de volgende opdracht:
```bash
dotnet run
```

De API zal beschikbaar zijn op `https://localhost:5001`.

## Endpoints

- **Attracties:** `/api/attractions`
- **Onderhoud:** `/api/maintenance`
- **Gebieden:** `/api/areas`
- **Medewerkers:** `/api/employees`
- **Bezoekersaantallen:** `/api/visitorcounts`

## Voorbeeldcode

Hier is een voorbeeld van de configuratie in `Program.cs`:
```csharp
using Microsoft.EntityFrameworkCore;
using Toverland_Api.Data;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Toverland API", Version = "v1" });
});

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    dbContext.Seed();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toverland API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Use CORS middleware
app.UseCors("AllowAll");

app.MapControllers();

app.Run();

// Custom TimeSpan converter
public class TimeSpanConverter : JsonConverter<TimeSpan?>
{
    public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (TimeSpan.TryParse(reader.GetString(), out var timeSpan))
            {
                return timeSpan;
            }
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString("c"));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
```

## Bijdragen

Zie de bijdragegids voor meer informatie over hoe je kunt bijdragen aan dit project.

## Licentie

Dit project is gelicentieerd onder de MIT-licentie.
```

Je kunt deze code in een README.md-bestand plaatsen om de documentatie van je project te verbeteren.
