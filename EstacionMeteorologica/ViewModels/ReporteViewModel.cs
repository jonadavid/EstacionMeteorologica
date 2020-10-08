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



        List<ResponseReporteFecha> reportes;

        string margenRadioButton = string.Empty;
        public string MargenRadioButton
        {
            get { return margenRadioButton; }
            set { SetProperty(ref margenRadioButton, value); }
        }

        #region[exportar a excel declaracion de variables]
        /*exportar a excel */
        public ICommand ExportToExcelCommand { private set; get; }
        private ExcelService excelService;
        #endregion
        public ReporteViewModel(RequestReporteFechas request)
        {
            //string quryStrings =
            //$"provincia={request.Provincia}&ciudad={request.Ciudad}&fechaInicio={request.FechaInicio}&fechaFin={request.FechaFin}&horaInicio={request.HoraInicio}&horaFin={request.HoraFin}";

            Title = "Reporte";
            MargenRadioButton = "40";

            ExportToExcelCommand = new Command(async () =>
            {

                await GetDataReporteFechas("http://estacionclimaiot-001-site1.etempurl.com/api/GetFiltraPorFechaHora?provincia=1&ciudad=1&fechaInicio=2020%2F05%2F01&horaInicio=00%3A00%3A00&horaFin=23%3A59%3A59");
              await ExportToExcel();
            });
            excelService = new ExcelService();




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
                    reporte.NombreProvincia

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


    }
}