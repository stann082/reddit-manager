using lib;
using MongoDB.Driver;
using Serilog;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
var pathToConfig = Path.Combine(Directory.GetCurrentDirectory(), "config");

var configuration = new ConfigurationBuilder()
    .SetBasePath(pathToConfig)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
    .Build();

Log.Logger = LoggingManager.Initialize();
var builder = WebApplication.CreateBuilder(args);

// Apply the configuration to the builder
builder.Configuration.AddConfiguration(configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton(_ => new MongoClient("mongodb://localhost:27017").GetDatabase("reddit"));
builder.Services.AddSingleton<SavedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
