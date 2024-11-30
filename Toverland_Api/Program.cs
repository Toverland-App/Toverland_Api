
using Toverland_Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Voeg services toe aan de container.
builder.Services.AddControllers();

// Voeg Swagger toe voor API-documentatie
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configureer MySQL en voeg ApplicationDbContext toe
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

var app = builder.Build();

// Configureer de HTTP-pijplijn.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
