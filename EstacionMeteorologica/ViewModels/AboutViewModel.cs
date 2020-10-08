using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EstacionMeteorologica.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("http://www.cisc.ug.edu.ec/cisc/index.html"));
        }

        public ICommand OpenWebCommand { get; }
    }
}