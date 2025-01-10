using Microsoft.EntityFrameworkCore;
using Toverland_Api.Data;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Toverland_Api.Services;

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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

// Register DataDumpService
builder.Services.AddTransient<DataDumpService>();

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
    var dataDumpService = scope.ServiceProvider.GetRequiredService<DataDumpService>();
    dbContext.Database.Migrate();
    await dataDumpService.TruncateTablesAsync();
    dbContext.Seed();
}

// Handle dumpdata command
if (args.Length > 0 && args[0] == "dumpdata")
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var dataDumpService = services.GetRequiredService<DataDumpService>();

    var filePath = args.Length > 1 ? args[1] : "data_dump.json";
    await dataDumpService.DumpDataAsync(filePath);
    Console.WriteLine($"Data dumped to {filePath}");
}
else
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    // Add Basic Authentication Middleware
    app.UseMiddleware<BasicAuthMiddleware>("DigitaleTovenaars", "password");

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toverland API v1"));

    app.UseHttpsRedirection();
    app.UseAuthorization();

    // Use CORS middleware
    app.UseCors("AllowAll");

    app.MapControllers();

    app.Run();
}

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



