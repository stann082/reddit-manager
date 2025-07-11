using System;
using lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Serilog;
using StackExchange.Redis;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = LoggingManager.Initialize(configuration);
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Apply the configuration to the builder
builder.Configuration.AddConfiguration(configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<SavedService>();

var hostname = builder.Configuration["Mongo:Hostname"];
var client = new MongoClient($"mongodb://{hostname}:27017");
builder.Services.AddSingleton(_ => client.GetDatabase("reddit"));

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect($"{hostname}:6379"));
builder.Services.AddScoped(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

var app = builder.Build();
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

try
{
    Log.Information("Starting web host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
