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
        public AsyncRelayCommand UpdateCommand { get; }
        
        [ObservableProperty]
        private object currentView;

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;
            UpdateCommand = new AsyncRelayCommand(SearchButtonPressed);
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

        [RelayCommand]
        public async Task SearchButtonPressed()
        {
            
            await UpdateCurrentData();
            await HomeVM.UpdateWeeklyData(SearchBoxText);
        }

        public async Task UpdateCurrentData()
        {
            var dataInfo = await WeatherData.LoadData(SearchBoxText);

            CurrentTemp = $"{dataInfo.current.temp_c}";
            ConditionText = $"{dataInfo.current.condition.text}";
            LocationName = $"{dataInfo.location.name}";

            DateAndTime = $"{dataInfo.location.localtime}";
            DateTime dateTime = DateTime.ParseExact(DateAndTime, "yyyy-MM-dd HH:mm", null);
            string formatDateTime = dateTime.ToString("dddd, HH:mm");
            DateAndTime = formatDateTime;

            StatusImage = $"http:{dataInfo.current.condition.icon}";
        }

    }
}
