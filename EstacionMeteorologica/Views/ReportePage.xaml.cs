using EstacionMeteorologica.Models;
using EstacionMeteorologica.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EstacionMeteorologica.Views
{
    public partial class ReportePage : ContentPage
    {

        RequestReporteFechas request;
        public ReportePage()
        {
            InitializeComponent();
            BindingContext =  new ReporteViewModel(request);//para obtener la fuente de datos
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
        

    }



}
