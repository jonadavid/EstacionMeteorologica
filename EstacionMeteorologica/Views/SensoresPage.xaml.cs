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
    public partial class SensoresPage : ContentPage
    {
        

        public SensoresPage()
        {
            InitializeComponent();
            BindingContext =  new SensoresViewModel(SensoresChart);//para obtener la fuente de datos
         
          
        }
       

      

        protected override void OnAppearing()
        {
            base.OnAppearing();



            
            

        }

       
    }



}
