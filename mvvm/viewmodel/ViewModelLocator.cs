using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.mvvm.viewmodel
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => Ioc.Default.GetService<MainViewModel>();
    }
}
