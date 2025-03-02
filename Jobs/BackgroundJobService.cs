using Sam.CarsTelegramBot.Services.Persistence;
using Sam.CarsTelegramBot.Services.Services;
namespace Sam.CarsTelegramBot.Services.Jobs;


public class BackgroundJobService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;

            Console.WriteLine("Background job is running at: " + now);
            if (now.Hour == 8)
            {
                Data.RefreshData();
            }
            if (now.Hour == 10)
            {
                var welcomeMessage = ChanelMessageService.WellCome();
                var carsMessage = ChanelMessageService.Cars();

                if (carsMessage.Any())
                {
                    await TelegramService.SendMessageToChanel(welcomeMessage);

                    foreach (var item in carsMessage)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                        await TelegramService.SendMessageToChanel(item);
                    }
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);

        }
    }
}
