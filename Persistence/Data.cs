namespace Sam.CarsTelegramBot.Services.Persistence
{
    public class Data
    {
        private static readonly HttpClient HttpClient = new();
        private static List<Car> cars;

        public static List<Car> TryGetData()
        {
            if (cars is null || !cars.Any())
                RefreshData();

            return cars;
        }

        public static void RefreshData()
        {
            var response = HttpClient.GetFromJsonAsync<DailyCarsResponseDto>("https://khodro45.com/api/v1/pricing/dailycars/?page_size=1000").Result;

            if (response?.count > 0)
            {

                cars = response.results
                    .SelectMany(p => p.dailycars.Select(x => new Car()
                    {
                        name = p.title,
                        bazar = x.price.ToString("N0"),
                        moshakhasat = $"{p.title} {x.car_properties.model.title} {x.car_properties.trim.title} مدل {x.car_properties.year.title}",
                    }))
                    .ToList();
            }
        }
    }
    public class Car
    {
        public string name { get; set; }
        public string moshakhasat { get; set; }
        public string bazar { get; set; }
    }
    public class DailyCarsResponseDto
    {
        public int count { get; set; }
        public List<Result> results { get; set; }
    }

    public class Result
    {
        public string title { get; set; }
        public string title_en { get; set; }
        public string url_slug { get; set; }
        public string slug { get; set; }
        public string logo { get; set; }
        public Dailycar[] dailycars { get; set; }
    }

    public class Dailycar
    {
        public string slug { get; set; }
        public string dailycar_slug { get; set; }
        public long price { get; set; }
        public string update_date { get; set; }
        public string type { get; set; }
        public float price_change { get; set; }
        public Car_Properties car_properties { get; set; }
    }

    public class Car_Properties
    {
        public Brand brand { get; set; }
        public Model model { get; set; }
        public Trim trim { get; set; }
        public Year year { get; set; }
        public Option option { get; set; }
    }

    public class Brand
    {
        public string title { get; set; }
        public string title_en { get; set; }
        public string url_slug { get; set; }
        public int id { get; set; }
        public string slug { get; set; }
    }

    public class Model
    {
        public string title { get; set; }
        public string title_en { get; set; }
        public string url_slug { get; set; }
        public int id { get; set; }
        public string slug { get; set; }
    }

    public class Trim
    {
        public string title { get; set; }
        public string title_en { get; set; }
        public string url_slug { get; set; }
        public int id { get; set; }
        public object slug { get; set; }
    }

    public class Year
    {
        public string title { get; set; }
        public int id { get; set; }
    }

    public class Option
    {
        public string title { get; set; }
        public int? id { get; set; }
    }

}
