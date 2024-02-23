using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weather.mvvm.model
{
    public class WeeklyWeatherAPI
    {

        public class Weather
        {
            [JsonPropertyName("forecast")]
            public Forecast? Forecast { get; set; }
        }

        public class Condition
        {
            [JsonPropertyName("text")]
            public string? Text { get; set; }
            [JsonPropertyName("icon")]
            public string? Icon { get; set; }
            [JsonPropertyName("code")]
            public int? Code { get; set; }
        }

        public class Forecast
        {
            [JsonPropertyName ("forecastday")]
            public List<Forecastday>? ForecastDay { get; set; }
        }

        public class Forecastday
        {
            [JsonPropertyName("date")]
            public DateOnly Date { get; set; }
            [JsonPropertyName("hour")]
            public List<Hour>? Hour { get; set; }
        }


        public class Hour
        {
            public long time_epoch { get; set; }
            [JsonPropertyName("time")]
            public DateTime Time { get; set; }
            public float temp_c { get; set; }
            public float temp_f { get; set; }
            public int is_day { get; set; }
            public float wind_mph { get; set; }
            public float wind_kph { get; set; }
            public int wind_degree { get; set; }
            public string wind_dir { get; set; }
            public int pressure_mb { get; set; }
            public float pressure_in { get; set; }
            public float precip_mm { get; set; }
            public float precip_in { get; set; }
            public int snow_cm { get; set; }
            public int humidity { get; set; }
            public int cloud { get; set; }
            public float feelslike_c { get; set; }
            public float feelslike_f { get; set; }
            public float windchill_c { get; set; }
            public float windchill_f { get; set; }
            public float heatindex_c { get; set; }
            public float heatindex_f { get; set; }
            public float dewpoint_c { get; set; }
            public float dewpoint_f { get; set; }
            public int will_it_rain { get; set; }
            public int chance_of_rain { get; set; }
            public int will_it_snow { get; set; }
            public int chance_of_snow { get; set; }
            public float vis_km { get; set; }
            public int vis_miles { get; set; }
            public float gust_mph { get; set; }
            public float gust_kph { get; set; }
            public int uv { get; set; }
            public float short_rad { get; set; }
            public float diff_rad { get; set; }
        }
    }
}
