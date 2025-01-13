using Microsoft.EntityFrameworkCore;
using Toverland_Api.Data;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Toverland_Api.Services;
using Microsoft.Extensions.Logging;

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

// Obtain the logger from the DI container
var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Starting application...");

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // Ensure the database is created and migrations are applied
    context.Seed();
}

// Handle the --dumpdata argument
if (args.Length > 0 && args[0] == "--dumpdata")
{
    if (args.Length < 2)
    {
        Console.WriteLine("Please provide a file name for the data dump.");
        return;
    }

    var fileName = args[1];
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dataDumpService = services.GetRequiredService<DataDumpService>();
        dataDumpService.DumpDataAsync(fileName).Wait();
        Console.WriteLine($"Data dumped to {fileName}");
    }
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

    logger.LogInformation("Starting web host...");
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
