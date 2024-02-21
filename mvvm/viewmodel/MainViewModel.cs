using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [RelayCommand]
        public async Task SearchButtonPressed()
        {
            var dataInfo = await WeatherData.LoadData(SearchBoxText);

            CurrentTemp = $"{dataInfo.current.temp_c}";
        }


    }
}
