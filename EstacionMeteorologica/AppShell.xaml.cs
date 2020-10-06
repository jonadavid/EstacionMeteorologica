using System;
using System.Collections.Generic;
using EstacionMeteorologica.ViewModels;
using EstacionMeteorologica.Views;
using Xamarin.Forms;

namespace EstacionMeteorologica
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
        

    }
}
