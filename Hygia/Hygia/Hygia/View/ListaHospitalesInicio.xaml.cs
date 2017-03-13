using Hygia.Model;
using Hygia.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hygia.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaHospitalesInicio : ContentPage
    {

        ObservableCollection<Hospital> ListaHospitales = new ObservableCollection<Hospital>();
        public ListaHospitalesInicio()
        {
            InitializeComponent();
            var HospitalesList = new HospitalesViewModel();
            ListaHospitales = new ObservableCollection<Hospital>(HospitalesList.ListaHospitales);
            ListHospitales.ItemsSource = ListaHospitales;

            //ListHospitales.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            //{
            //    if (e.Item == null) return;

            //    ((ListView)sender).SelectedItem = null;
                
            //};
        }

        public void HospitalClick(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            Hospital selected = (Hospital)e.Item;
            ((ListView)sender).SelectedItem = null;
            
            Navigation.PushAsync(new HospitalInfo(selected));
        }
    }
}

   