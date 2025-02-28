using System.Text.Json;

namespace Sam.CarsTelegramBot.Services.Persistence
{
    public class Data
    {
        private static HttpClient httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };
        private static List<Car> cars;

        public static List<Car> TryGetData()
        {
            if (cars is null || !cars.Any())
                RefreshData();

            return cars;
        }

        public static void RefreshData()
        {
            var response = httpClient.GetFromJsonAsync<CarResponseDto>("https://freewebservices.liara.run/Car/car_price").Result;

            if (response.Ok)
                cars = response.Result;
        }
    }

    public class CarResponseDto
    {
        public bool Ok { get; set; }
        public List<Car> Result { get; set; }
    }

    public class Car
    {
        public string name { get; set; }
        public string moshakhasat { get; set; }
        public string karkhane { get; set; }
        public string bazar { get; set; }
    }

}


