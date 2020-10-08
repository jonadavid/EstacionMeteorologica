using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EstacionMeteorologica.ViewModels
{
    public class ReporteViewModel : BaseViewModel
    {

        string margenRadioButton = string.Empty;
        public string MargenRadioButton
        {
            get { return margenRadioButton; }
            set { SetProperty(ref margenRadioButton, value); }
        }
        public ReporteViewModel()
        {
            Title = "Reporte";
            MargenRadioButton = "40";
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("http://www.cisc.ug.edu.ec/cisc/index.html"));
        }

      //  public ICommand OpenWebCommand { get; }
    }
}