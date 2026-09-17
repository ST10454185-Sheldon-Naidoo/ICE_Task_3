using FactoryApi.Models;
using FactoryApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Registers the TelemetryService dependency
builder.Services.AddSingleton<TelemetryService>();

var app = builder.Build();

// Configures the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Factory Telemetry Endpoint (Point 1)
app.MapGet("/api/telemetry", (TelemetryService service) =>
{
    var readings = service.GenerateReadings();
    int anomalyCount = service.CountAnomaliesRecursive(readings);
    var hottestMachine = service.FindHottestMachineRecursive(readings);

    return Results.Ok(new ApiResponse
    {
        Readings = readings,
        TotalAnomalies = anomalyCount,
        HottestMachineName = hottestMachine?.MachineName ?? "N/A"
    });
})
.WithName("GetFactoryTelemetry");

app.Run();
