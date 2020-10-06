using System.ComponentModel;
using Xamarin.Forms;
using EstacionMeteorologica.ViewModels;

namespace EstacionMeteorologica.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}