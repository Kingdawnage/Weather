using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Text.Json;
using Weather.mvvm.model;
using static Weather.mvvm.model.DataClass;

namespace Weather.mvvm.viewmodel
{
    public partial class MainViewModel : ObservableRecipient
    {
        public HomeViewModel HomeVM { get; set; }

        [ObservableProperty]
        private object currentView;

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;
        }

        [ObservableProperty]
        private string searchBoxText;
        [ObservableProperty]
        private string currentTemp;
        [ObservableProperty]
        private string conditionText;
        [ObservableProperty]
        private string locationName;
        [ObservableProperty]
        private string dateAndTime;
        [ObservableProperty]
        private string statusImage;
        [ObservableProperty]
        string uvIndex;
        [ObservableProperty]
        string windSpeed;
        [ObservableProperty]
        string humidity;
        [ObservableProperty]
        string pressure;
        [ObservableProperty]
        string sunImage;
        [ObservableProperty]
        string monImage;
        [ObservableProperty]
        string tueImage;
        [ObservableProperty]
        string wedImage;
        [ObservableProperty]
        string thuImage;
        [ObservableProperty]
        string friImage;
        [ObservableProperty]
        string satImage;
        [ObservableProperty]
        string sunTemp;
        [ObservableProperty]
        string monTemp;
        [ObservableProperty]
        string tueTemp;
        [ObservableProperty]
        string wedTemp;
        [ObservableProperty]
        string thuTemp;
        [ObservableProperty]
        string friTemp;
        [ObservableProperty]
        string satTemp;

        [RelayCommand]
        public void SearchButtonPressed()
        {
            Task _ = UpdateCurrentData();
            
        }

        public async Task UpdateCurrentData()
        {
            var dataInfo = await WeatherData.LoadData(SearchBoxText);
            var weather = await LoadWeeklyData(SearchBoxText);

            CurrentTemp = $"{dataInfo.current.temp_c}";
            ConditionText = $"{dataInfo.current.condition.text}";
            LocationName = $"{dataInfo.location.name}";

            DateAndTime = $"{dataInfo.location.localtime}";
            DateTime dateTime = DateTime.ParseExact(DateAndTime, "yyyy-MM-dd HH:mm", null);
            string formatDateTime = dateTime.ToString("dddd, HH:mm");
            DateAndTime = formatDateTime;

            StatusImage = $"http:{dataInfo.current.condition.icon}";

            UvIndex = $"{dataInfo.current.uv}";
            WindSpeed = $"{dataInfo.current.wind_kph}";
            Humidity = $"{dataInfo.current.humidity}";
            Pressure = $"{dataInfo.current.pressure_mb}";

            // Weekly data
            var forecastDays = weather.Forecast.ForecastDay
                .OrderBy(fd => (int)fd.Date.DayOfWeek) // Order by day of the week starting from Sunday (Sunday: 0, Monday: 1, ..., Saturday: 6)
                .ToList();

            float[] temperatures = new float[forecastDays.Count];
            string[] icons = new string[forecastDays.Count];

            int index = 0;
            foreach (var forecastDay in forecastDays)
            {
                var firstHour = forecastDay.Hour.FirstOrDefault();
                temperatures[index] = firstHour.TempC;
                icons[index] = firstHour.Condition?.Icon;
                index++;
            }
            SunImage = $"http:{icons[0]}";
            MonImage = $"http:{icons[1]}";
            TueImage = $"http:{icons[2]}";
            WedImage = $"http:{icons[3]}";
            ThuImage = $"http:{icons[4]}";
            FriImage = $"http:{icons[5]}";
            SatImage = $"http:{icons[6]}";

            SunTemp = $"{temperatures[0]}";
            MonTemp = $"{temperatures[1]}";
            TueTemp = $"{temperatures[2]}";
            WedTemp = $"{temperatures[3]}";
            ThuTemp = $"{temperatures[4]}";
            FriTemp = $"{temperatures[5]}";
            SatTemp = $"{temperatures[6]}";

        }

        public static async Task<WeeklyWeatherAPI.Weather>LoadWeeklyData(string search)
        {
            UriBuilder uriBuilder = new UriBuilder("https://api.weatherapi.com/v1/forecast.json");
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            query["key"] = "382c30f637a04587915104833241502";
            query["q"] = search;
            query["days"] = "7";
            uriBuilder.Query = query.ToString();

            string url = uriBuilder.ToString();
            //string url = $"https://api.weatherapi.com/v1/forecast.json?key=382c30f637a04587915104833241502&q={escapedSearch}&days=7";
            var json = await new HttpClient().GetStringAsync(url);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonToDateTime());

            var weather = JsonSerializer.Deserialize<WeeklyWeatherAPI.Weather>(json, options);
            //var today = weather.Forecast.ForecastDay.FirstOrDefault(x => x.Date.Day == DateTime.Now.Day);
            //var time = today.Hour.FirstOrDefault(x => x.Time.Hour == DateTime.Now.Hour);

            return weather;
        }
    }
}
