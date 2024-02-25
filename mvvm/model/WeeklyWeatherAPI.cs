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
            [JsonPropertyName("time_epoch")]
            public long TimeEpoch { get; set; }
            [JsonPropertyName("time")]
            public DateTime Time { get; set; }
            [JsonPropertyName("temp_c")]
            public float TempC { get; set; }
            [JsonPropertyName("temp_f")]
            public float TempF { get; set; }

            [JsonPropertyName("is_day")]
            public long IsDay { get; set; }

            [JsonPropertyName("condition")]
            public Condition? Condition { get; set; }

            [JsonPropertyName("wind_mph")]
            public float WindMph { get; set; }

            [JsonPropertyName("wind_kph")]
            public float WindKph { get; set; }

            [JsonPropertyName("wind_degree")]
            public float WindDegree { get; set; }

            [JsonPropertyName("wind_dir")]
            public string? WindDir { get; set; }

            [JsonPropertyName("pressure_mb")]
            public float PressureMb { get; set; }

            [JsonPropertyName("pressure_in")]
            public float PressureIn { get; set; }

            [JsonPropertyName("precip_mm")]
            public float PrecipMm { get; set; }

            [JsonPropertyName("precip_in")]
            public float PrecipIn { get; set; }

            [JsonPropertyName("snow_cm")]
            public float SnowCm { get; set; }

            [JsonPropertyName("humidity")]
            public float Humidity { get; set; }

            [JsonPropertyName("cloud")]
            public float Cloud { get; set; }

            [JsonPropertyName("feelslike_c")]
            public float FeelslikeC { get; set; }

            [JsonPropertyName("feelslike_f")]
            public float FeelslikeF { get; set; }

            [JsonPropertyName("windchill_c")]
            public float WindchillC { get; set; }

            [JsonPropertyName("windchill_f")]
            public float WindchillF { get; set; }

            [JsonPropertyName("heatindex_c")]
            public float HeatindexC { get; set; }

            [JsonPropertyName("heatindex_f")]
            public float HeatindexF { get; set; }

            [JsonPropertyName("dewpoint_c")]
            public float DewpointC { get; set; }

            [JsonPropertyName("dewpoint_f")]
            public float DewpointF { get; set; }

            [JsonPropertyName("will_it_rain")]
            public float WillItRain { get; set; }

            [JsonPropertyName("chance_of_rain")]
            public float ChanceOfRain { get; set; }

            [JsonPropertyName("will_it_snow")]
            public float WillItSnow { get; set; }

            [JsonPropertyName("chance_of_snow")]
            public float ChanceOfSnow { get; set; }

            [JsonPropertyName("vis_km")]
            public float VisKm { get; set; }

            [JsonPropertyName("vis_miles")]
            public float VisMiles { get; set; }

            [JsonPropertyName("gust_mph")]
            public float GustMph { get; set; }

            [JsonPropertyName("gust_kph")]
            public float GustKph { get; set; }

            [JsonPropertyName("uv")]
            public float Uv { get; set; }

            [JsonPropertyName("short_rad")]
            public float ShortRad { get; set; }

            [JsonPropertyName("diff_rad")]
            public float DiffRad { get; set; }
        }
    }
}
