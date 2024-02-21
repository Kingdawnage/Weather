using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Weather.mvvm.model
{
    internal class WeatherData
    {
        public static async Task<DataClass.Rootobject> LoadData(string search)
        {
            string url = $"https://api.weatherapi.com/v1/current.json?key=382c30f637a04587915104833241502&q={Uri.EscapeDataString(search)}";

            try
            {
                var response = await ApiHelper.ApiClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var value = await response.Content.ReadFromJsonAsync<DataClass.Rootobject>();

                return value;

            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"HTTP request failed {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JSON decentralization failed {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured {ex.Message}");
                throw;
            }

        }
    }
}
