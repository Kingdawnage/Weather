using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
        private BitmapImage fixedStatusImage;
        [ObservableProperty]
        string sunImage;
        [ObservableProperty]
        BitmapImage sunStatusImage;


        [RelayCommand]
        public void SearchButtonPressed()
        {
            Task _ = UpdateCurrentData();
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
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(StatusImage, UriKind.Absolute);
            bitmapImage.EndInit();
            FixedStatusImage = bitmapImage;
        }
    }
}
