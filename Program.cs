using Sam.CarsTelegramBot.Services.Infrastructures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterTelegramBot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/", () => "healthy");

app.Run();
