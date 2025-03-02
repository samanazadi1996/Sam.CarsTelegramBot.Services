using Sam.CarsTelegramBot.Services.Persistence;
using System.Globalization;

namespace Sam.CarsTelegramBot.Services.Services;

public class ChanelMessageService
{
    public static string WellCome()
    {
        PersianCalendar pc = new PersianCalendar();
        DateTime now = DateTime.Now;
        string today = $"{pc.GetYear(now)}/{pc.GetMonth(now):00}/{pc.GetDayOfMonth(now):00}";

        return $"🚗✨ سلام دوستان عزیز! روز بخیر 🌞 امروز {today}، قیمت به‌روز خودروها رو با شما در کانال به اشتراک می‌ذاریم. همراه ما باشید! 📢📊";
    }


    public static List<string> Cars()
    {
        var model = Data.TryGetData();
        List<string> list = [];

        if (!model.Any()) return list;

        foreach (var item in model.GroupBy(p => p.name))
        {
            var message = $"\ud83d\ude97 {item.Key}" + Environment.NewLine + Environment.NewLine;

            foreach (var car in item)
                message += $"\ud83d\udccb {car.moshakhasat}\r\n\ud83d\udcb8 قیمت بازار: {car.bazar:N0} تومان" + Environment.NewLine + Environment.NewLine;

            list.Add(message);
        }

        return list;
    }

}