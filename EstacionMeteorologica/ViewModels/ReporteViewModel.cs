using EstacionMeteorologica.Models;
using EstacionMeteorologica.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Reflection;
using System.Numerics;
using System.Linq;

namespace EstacionMeteorologica.ViewModels
{
    public class ReporteViewModel : BaseViewModel
    {



        private List<ResponseReporteFecha> reportes;
        private List<Provincia> provincias;
        private List<Ciudad> ciudades;

        private Picker provincia;
        private Picker ciudad;

        string margenRadioButton = string.Empty;
        public string MargenRadioButton
        {
            get { return margenRadioButton; }
            set { SetProperty(ref margenRadioButton, value); }
        }

        #region[exportar a excel declaracion de variables]
        /*exportar a excel */
        public ICommand ExportToExcelCommand { private set; get; }

        

        public Picker Provincia
        {
            get => provincia; set
            {
                provincia = value;
                OnPropertyChanged();
            }
        }

        public Picker Ciudad
        {
            get => ciudad; set
            {
                ciudad = value;
                OnPropertyChanged();
            }
        }

        private ExcelService excelService;
        #endregion
        public ReporteViewModel()
        {



            //string quryStrings =
            //$"provincia={request.Provincia}&ciudad={request.Ciudad}&fechaInicio={request.FechaInicio}&fechaFin={request.FechaFin}&horaInicio={request.HoraInicio}&horaFin={request.HoraFin}";
            Provincia = provincia;
            Ciudad = ciudad;
            Title = "Reporte";
            MargenRadioButton = "40";


           // GetDataProvincia("http://estacionclimaiot-001-site1.etempurl.com/api/Provincia");


        }

        public ReporteViewModel(List<ResponseReporteFecha> reporte)
        {

            reportes = reporte;

            if (reporte != null)
            {
                excelService = new ExcelService();

                ExportToExcel().ConfigureAwait(true).GetAwaiter();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "No existe datos!", "Aceptar");
            }

            //string quryStrings =
            //$"provincia={request.Provincia}&ciudad={request.Ciudad}&fechaInicio={request.FechaInicio}&fechaFin={request.FechaFin}&horaInicio={request.HoraInicio}&horaFin={request.HoraFin}";





            //  GetDataReporteFechas(url).ConfigureAwait(true).GetAwaiter();
           



        }


        #region[Consumo de servico]

        private async Task GetDataReporteFechas(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResponseReporteFecha>>(jsonResult);
            reportes = result;
           
        }

       

        private async void GetDataCuidad(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Ciudad>>(jsonResult);
           // ciudad = result;


        }


        #endregion

        #region[usando reflexiones para obtner la cantidad de campos y sus nombres desde una clase T]

        List<string> nombresCampos;
        public  async Task NombresCamposClase(Type t)
        {
            nombresCampos = new List<string>();
            System.Reflection.PropertyInfo[] nombresVariables = t.GetProperties();
            foreach (var nombres in nombresVariables)
            {
                nombresCampos.Add(nombres.Name);
            }
           
        }

        #endregion


        #region[exportar a excel metodos]
        /*exportar a excel */
        private async Task ExportToExcel()
        {
            var fileName = $"{"Reporte_"+String.Format("{0:u}", DateTime.Now)}.xlsx";
            var reemplazar = fileName.Replace("/","_").Replace(":","_").Replace("?","_").Replace('"'.ToString(),"_").Replace("<","_").Replace(">","_").Replace("|","_").Replace("Z","");

            string filePath = excelService.GenerateExcel(reemplazar);
          
          
                Type t = typeof(ResponseReporteFecha);
                await NombresCamposClase(t);
                List<String> header = nombresCampos;
            
           
            var data = new ExcelData();
            data.Headers = header;

            foreach (var reporte in reportes)
            {
                var row = new List<string>()
                {
                    reporte.NombreProvincia,
                    reporte.Ciudad,
                    reporte.Id.ToString(),
                    reporte.Temperatura.ToString(),
                    reporte.Humedad.ToString(),
                    reporte.Precipitacion_lluvia.ToString(),
                    reporte.Radiacion.ToString(),
                    reporte.Sensacion_termica.ToString(),
                    String.Format("{0:u}",reporte.Fecha),
                    reporte.Id_estacion.ToString(), 

                };

                data.Values.Add(row);
            }

            excelService.InsertDataIntoSheet(filePath, "Publications", data);

            await Launcher.OpenAsync(new OpenFileRequest()
            {
                File = new ReadOnlyFile(filePath)
            });
        }
        #endregion


        /*vistas */

      
       

    }
}