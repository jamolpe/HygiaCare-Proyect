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
        WorkingMaps MapsGest;
		String distancia;
		String Tiempo;
		HospitalesViewModel HospitalesList = new HospitalesViewModel();
        ObservableCollection<Hospital> ListaHospitales = new ObservableCollection<Hospital>();
        public ListaHospitalesInicio()
        {
            InitializeComponent();
	    
	    ObtenerTodoslosHospitales();

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

		public async void ObtenerTodoslosHospitales(){
			if (await HospitalesList.ObtenerHospitales())
			{
				ListaHospitales = new ObservableCollection<Hospital>(HospitalesList.ListaHospitales);
				if(ListaHospitales.Count !=0){
					ListHospitales.ItemsSource = ListaHospitales;
					ACCargandoHosp.IsRunning = false;
                    //reordenarLista();
				}else{
					await DisplayAlert("Aviso","No se han encontrado hospitales","ok");
				}

			}

		}

        /*
        public async void reordenarLista(){
            int id = 0;
            foreach(Hospital hosp in ListaHospitales){
				MapsGest = new WorkingMaps();

				if (await MapsGest.GetActualPosition())
				{
					await MapsGest.GetJSONDirecciones(hosp.coordenadaX, hosp.coordenadaY, hosp.coordenadaZ);
					distancia = MapsGest.GetDistancia();
                    ListaHospitales[id].distancia = distancia;
					Tiempo = MapsGest.GetTiempo();
                    ordenlista();
				}

                id++;
            }
        }

        public void ordenlista(){


            HospitalesList.ListaHospitales = (from Hospital item in HospitalesList.ListaHospitales
                                              orderby item.distancia ascending
                               select item).ToList();
            ListaHospitales = new ObservableCollection<Hospital>(HospitalesList.ListaHospitales);
			ListHospitales.ItemsSource = ListaHospitales;

			ACCargandoHosp.IsRunning = false;
        }
        */
    }
}

   