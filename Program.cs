using Sam.CarsTelegramBot.Services.Jobs;
using Sam.CarsTelegramBot.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureTelegramBot();

builder.Services.RegisterTelegramBot(builder.Configuration);

builder.Services.AddHostedService<BackgroundJobService>();

var app = builder.Build();

app.MapGet("/", () => "healthy");

app.Run();