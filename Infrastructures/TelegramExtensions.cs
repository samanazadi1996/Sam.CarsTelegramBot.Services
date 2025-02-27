using Sam.CarsTelegramBot.Services.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Sam.CarsTelegramBot.Services.Infrastructures
{
    public static class TelegramExtensions
    {
        private static TelegramBotClient? _bot;

        public static IServiceCollection RegisterTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            _bot = new TelegramBotClient(configuration["TelegramBotToken"]!);

            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = [] // دریافت همه‌ی آپدیت‌ها
            };

            _bot.StartReceiving(
                updateHandler: HandleUpdateAsync,
                errorHandler: HandleErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );


            return services;
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message is not null && update.Message.Type == MessageType.Text && !string.IsNullOrEmpty(update.Message.Text))
            {
                Console.WriteLine($"[{update.Message.Chat.FirstName}]: {update.Message.Text}");

                if (update.Message.Text == "/start")
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, MessageService.WellCome(), cancellationToken: cancellationToken);
                }
                else if (update.Message.Text == "/cars")
                {
                    var response = MessageService.Cars();
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, response.message, replyMarkup: response.inlineKeyboard);
                }
                else
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, MessageService.Cars(update.Message.Text), cancellationToken: cancellationToken);
                }


            }
            else if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery is not null && !string.IsNullOrEmpty(update.CallbackQuery.Data))
            {
                await botClient.SendTextMessageAsync(update.CallbackQuery.From.Id, MessageService.Cars(update.CallbackQuery.Data), cancellationToken: cancellationToken);
            }
        }

        private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"خطا رخ داد: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
