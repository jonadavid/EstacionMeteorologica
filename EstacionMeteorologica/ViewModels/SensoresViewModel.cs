using EstacionMeteorologica.Models;
using EstacionMeteorologica.Views;
using Microcharts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EstacionMeteorologica.ViewModels
{
    public class SensoresViewModel : BaseViewModel
    {
        private DatosSensores datos;
        private List<ChartEntry> entradas;

       

        

       

        public  List<ChartEntry> Entries 
        { get => entradas;
            set
            {
                entradas = value;
                OnPropertyChanged();
            }
        }

        private Microcharts.Chart chart;
        public Microcharts.Chart Chart
        {
            get => chart;
            set
            {
                chart = value;
                OnPropertyChanged();
            }
        }


        public DatosSensores Datos
        {
            get => datos; set
            {
                datos = value;
                OnPropertyChanged();
            }
        }

        private Microcharts.Forms.ChartView Sensor;

        public ICommand ActualizarCommand { get; set; }//para ejecutar la consulta al servicio rest
        public SensoresViewModel(Microcharts.Forms.ChartView sensoresChart)
        {
            Sensor = sensoresChart;
            Title = "Datos Meteorológicos";
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync(""));
            GetDataClima("http://estacionclimaiot-001-site1.etempurl.com/api/GetUltimoDatoIngresado");
            ActualizarCommand = new Command(async () =>
            {

                 GetDataClima("http://estacionclimaiot-001-site1.etempurl.com/api/GetUltimoDatoIngresado");

            });

        //    SensoresPagina = new SensoresPage();
            
            //     Datos = new ObservableCollection<DatosSensores>();
        }

        private async void GetDataClima(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DatosSensores>>(jsonResult);
            Datos = result.FirstOrDefault();
           
            var EntriesTmp = new List<ChartEntry>
            {
                 new ChartEntry(50)
                    {
                    Color = SkiaSharp.SKColor.Parse("#FE9A2E"),
                    
                    TextColor = SkiaSharp.SKColor.Parse("#8181F7"),
                    Label ="Temperatura",
                    ValueLabel= Datos.Temperatura.ToString(),
                    
                    
                    },


                    new ChartEntry(50)
                    {
                    Color = SkiaSharp.SKColor.Parse("#FE9A2E"),
                    TextColor = SkiaSharp.SKColor.Parse("#8181F7"),
                    Label ="Humedad",
                    ValueLabel= Datos.Humedad.ToString()
                    }

            };

            Entries = EntriesTmp;
            RadialGaugeChart chart = new RadialGaugeChart();
            chart.Entries = Entries;
            Sensor.Chart = chart;
        }






    }
}