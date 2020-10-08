using EstacionMeteorologica.Models;
using EstacionMeteorologica.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EstacionMeteorologica.Views
{
    public partial class ReportePage : ContentPage
    {
        

        public ReportePage()
        {
            InitializeComponent();
            BindingContext =  new ReporteViewModel();//para obtener la fuente de datos
            FechaFin.Date =  DateTime.Now;
            //FechaFin.MinimumDate = ;
            //FechaFin.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
         
            //FechaInicio.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //FechaInicio.MinimumDate = new DateTime(2020, 10, 10);




        }
       

       
    }



}
