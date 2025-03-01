using Sam.CarsTelegramBot.Services.Persistence;
namespace Sam.CarsTelegramBot.Services.Jobs;


public class BackgroundJobService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            if (now.Hour == 8)
            {
                Console.WriteLine("Background job is running at: " + now);

                Data.RefreshData();
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);

        }
    }
}
