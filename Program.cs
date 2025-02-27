using Sam.CarsTelegramBot.Services.Infrastructures;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterTelegramBot(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "healthy");

app.Run();
