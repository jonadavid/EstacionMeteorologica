using DocumentFormat.OpenXml.Vml.Spreadsheet;
using EstacionMeteorologica.Models;
using EstacionMeteorologica.ViewModels;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EstacionMeteorologica.Views
{
    public partial class ReportePage : ContentPage
    {

        RequestReporteFechas request;
        List<Ciudad> ciudad;
      
        public ReportePage()
        {
            InitializeComponent();
            BindingContext =  new ReporteViewModel(request, Provincia, Ciudad);//para obtener la fuente de datos
            FechaFin.Date =  DateTime.Now;
            SetearHoras();
            SetearMinutoSegundos();
           
            //FechaFin.MinimumDate = ;
            //FechaFin.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            //FechaInicio.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //FechaInicio.MinimumDate = new DateTime(2020, 10, 10);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetearHoras();
            SetearMinutoSegundos();
           

          
        }
        private void SetearHoras() { 

            for(int i = 0; i<24;i++ )
            {
                if (i < 10)
                {
                    HoraFin.Items.Add("0"+i.ToString());
                    HoraInicio.Items.Add("0"+i.ToString());
                }
                else
                {
                    HoraFin.Items.Add(i.ToString());
                    HoraInicio.Items.Add(i.ToString());
                }
            }
            HoraInicio.SelectedIndex = 0;
            HoraFin.SelectedIndex = 23;
        }

        private void SetearMinutoSegundos()
        {

            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    MinutoInicio.Items.Add("0"+i.ToString());
                    MinutoFin.Items.Add("0"+i.ToString());
                    SegundoInicio.Items.Add("0"+i.ToString());
                    SegundoFin.Items.Add("0"+i.ToString());
                }
                else {
                    MinutoInicio.Items.Add(i.ToString());
                    MinutoFin.Items.Add(i.ToString());
                    SegundoInicio.Items.Add(i.ToString());
                    SegundoFin.Items.Add(i.ToString());
                }

                
            }
            MinutoInicio.SelectedIndex = 0;
            MinutoFin.SelectedIndex = DateTime.Now.Minute;
            SegundoInicio.SelectedIndex = 0;
            SegundoFin.SelectedIndex = DateTime.Now.Second;

        }


       

        private async void GetDataCiudadProvincia(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Ciudad>>(jsonResult);
           
            ciudad = result;

            Ciudad.Items.Clear();
            if (ciudad.Count>0) {
               
            foreach (var item in ciudad)
                Ciudad.Items.Add(item.Nombre_Ciudad);

                Application.Current.Resources["SesionCiudad"] = result;

                
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", response.Content.ToString(), "Aceptar");
            }

        }

        private void Provincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
                           var picker = (Picker)sender;
              int selectedIndex = picker.SelectedIndex;

              if (selectedIndex != -1)
              {
                monkeyNameLabel.Text = (string)picker.ItemsSource[selectedIndex];
              }
             */

            try
            {

               // var picker = (Picker)sender;
                int selectedIndex = Provincia.SelectedIndex;

                if (selectedIndex != -1)
                {
                    var pro = Provincia.SelectedItem;

                    var sesionProvincia = (List<Provincia>)Application.Current.Resources["SesionProvincia"];

                    var valorIndice = sesionProvincia.Where(x => x.Nombre_Provincia.Equals(pro)).FirstOrDefault().Id;

                    var url = $"http://estacionclimaiot-001-site1.etempurl.com/api/GetCiudadProvincia?id_provincia={valorIndice}";
                    GetDataCiudadProvincia(url);
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", ex.Message, "Aceptar");
            }
        }
    }



}
