using Sam.CarsTelegramBot.Services.Persistence;
using Telegram.Bot.Types.ReplyMarkups;

namespace Sam.CarsTelegramBot.Services.Services;

public class MessageService
{
    public static string WellCome()
    {
        return @"سلام! خوش اومدی به بات ماشین‌های ما. من اینجا هستم تا به شما کمک کنم تا اطلاعات بروز درباره قیمت ماشین‌ها رو پیدا کنید.
چندتا گزینه دارم که می‌تونی ازشون استفاده کنی:
- برای مشاهده لیست تمام ماشین‌ها، روی دکمه 'لیست ماشین‌ها' کلیک کن یا از دستور زیر استفاده کن:
/cars

- اگر به دنبال اطلاعات خاصی در مورد یه ماشین هستی، می‌تونی نام اون ماشین رو وارد کنی تا قیمت و اطلاعات دقیق‌ترشو ببینی.

اگر سوالی داشتی یا کمکی نیاز داشتی، من همیشه اینجا هستم!";
    }
    public static (string message, InlineKeyboardMarkup inlineKeyboard) Cars()
    {
        var data = Data.TryGetData().DistinctBy(p => p.name);

        var inlineKeyboard = new InlineKeyboardMarkup(
            data.Select(p => new[]
            {
                    new InlineKeyboardButton
                    {
                        Text = p.name,
                        CallbackData = p.name ,
                    }
            }).ToArray()
        );

        var message = @"🚗 در اینجا لیستی از ماشین‌های موجود برای شما دارم. 
شما می‌توانید با استفاده از دکمه‌های زیر، اطلاعات بیشتر و قیمت هر ماشین را مشاهده کنید:";

        return (message, inlineKeyboard);
    }

    public static string Cars(string name)
    {
        var model = Data.TryGetData()
            .Where(p => p.moshakhasat.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (!model.Any())
        {
            return @"متاسفانه نتونستم ماشین مورد نظر شما رو پیدا کنم.
لطفاً نام دقیق ماشین رو وارد کنید و مطمئن شوید که از اسم درست استفاده کرده‌اید.
برای دیدن لیست تمام ماشین‌ها از دستور /cars استفاده کنید.";
        }

        var message = @"🚗 اطلاعات ماشین‌هایی که پیدا کردم:

" + string.Join(Environment.NewLine + Environment.NewLine, model.Select(p => @$"
🛻 {p.name}
📋 مشخصات: {p.moshakhasat}
💸 قیمت بازار: {p.bazar:N0} تومان"));

        message += "\r\n\nاگر ماشین دیگه‌ای می‌خواهید بررسی کنید، فقط نام اون رو وارد کنید یا از دستور /cars برای دیدن لیست ماشین‌ها استفاده کنید.";

        return message;
    }
}