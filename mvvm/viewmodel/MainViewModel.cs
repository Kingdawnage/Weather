using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Weather.mvvm.model;

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
        string sunImage;
        [ObservableProperty]
        string uvIndex;
        [ObservableProperty]
        string windSpeed;
        [ObservableProperty]
        string humidity;
        [ObservableProperty]
        string pressure;


        [RelayCommand]
        public void SearchButtonPressed()
        {
            Task _ = UpdateCurrentData();

        }

        public async Task UpdateCurrentData()
        {
            var dataInfo = await WeatherData.LoadData(SearchBoxText);
            //var test = await LoadWeeklyData(SearchBoxText);

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
        }

        public static async Task<DataClass.Rootobject> LoadWeeklyData(string search)
        {
            string url = $"https://api.weatherapi.com/v1/current.json?key=382c30f637a04587915104833241502&q={Uri.EscapeDataString(search)}";
            var json = await new HttpClient().GetStringAsync(url);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonToDateTime());

            var weather = JsonSerializer.Deserialize<DataClass.Rootobject>(json, options);
            var today = weather.forecast.Day.FirstOrDefault(x => x.date.Day == DateTime.Now.Day);
            var time = today.Hour.FirstOrDefault(x => x.time.Hour == DateTime.Now.Hour);

            Console.WriteLine($"{time.temp_c} {time.condition.icon}");
            Console.ReadKey();

            return null;
        }

    }
}
