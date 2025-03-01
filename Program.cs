using Sam.CarsTelegramBot.Services.Infrastructures;
using Sam.CarsTelegramBot.Services.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<BackgroundJobService>();
builder.Services.RegisterTelegramBot(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "healthy");

app.Run();
