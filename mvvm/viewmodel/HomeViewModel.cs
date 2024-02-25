using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Weather.mvvm.model;

namespace Weather.mvvm.viewmodel
{
    public partial class HomeViewModel : ObservableRecipient
    {
        public AsyncRelayCommand ExecuteUpdateWeeklyDataCommand { get;}
        public HomeViewModel()
        {
        }

        //private async Task UpdateWeeklyData()
        //{
        //    throw new NotImplementedException();
        //}

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

        public async Task UpdateWeeklyData(string searchBoxText)
        {
            var weather = await LoadWeeklyData(searchBoxText);

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

            UvIndex = $"{weather.Current.UV}";
            WindSpeed = $"{weather.Current.Wind_kph}";
            Humidity = $"{weather.Current.Humidity}";
            Pressure = $"{weather.Current.Pressure_mb}";
        }

        public static async Task<WeeklyWeatherAPI.Weather> LoadWeeklyData(string search)
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
