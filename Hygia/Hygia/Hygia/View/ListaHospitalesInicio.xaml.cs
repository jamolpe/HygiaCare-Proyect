﻿using Hygia.Model;
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
		private bool _isRefreshing = false;
		public bool IsRefreshing
		{
			get { return _isRefreshing; }
			set
			{
				_isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
			}
		}
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
            Navigation.PushAsync(new HospitalInfo(selected,ObtenerHospCercanos(selected)));
        }

        public List<Hospital> ObtenerHospCercanos(Hospital hosp){
            int i = 0;
            int posicion=0;
            int inicial = 0;
            int final = 0;
            List<Hospital> listaactual = new List<Hospital>();
            List<Hospital> lista = new List<Hospital>();
            foreach(Hospital hospital in ListHospitales.ItemsSource){
                if(hospital.id == hosp.id){
                    posicion = i;
                }
                listaactual.Add(hospital);
                i++;
            }

            if(posicion <= 3){
                inicial = 0;
            }else{
                inicial = posicion - 3;
            }

            if (i <= posicion + 3){
                final = i;
            }else{
                final = posicion + 3;
            }

            for (int j = inicial; j <= final; j++){
                if( listaactual[j].id != hosp.id){
                    lista.Add(listaactual[j]);
                }
            }
            return lista;

        }
		public async Task ObtenerTodoslosHospitales(){
			if (await HospitalesList.ObtenerHospitales())
			{
				ListaHospitales = new ObservableCollection<Hospital>(HospitalesList.ListaHospitales);
				if(ListaHospitales.Count !=0){
                    
					ListHospitales.ItemsSource = ListaHospitales;
					ACCargandoHosp.IsRunning = false;
                    reordenarLista();
				}else{
					await DisplayAlert("Aviso","No se han encontrado hospitales","ok");
				}

			}

		}


        public async void reordenarLista(){
            int id = 0;
            ObservableCollection<Hospital> ListaHospitalesOrdenacion = new ObservableCollection<Hospital>();
            foreach(Hospital hosp in ListHospitales.ItemsSource){
				MapsGest = new WorkingMaps();

				if (await MapsGest.GetActualPosition())
				{
					await MapsGest.GetJSONDirecciones(hosp.coordenadaX, hosp.coordenadaY, hosp.coordenadaZ);
					distancia = MapsGest.GetDistancia();
                    hosp.distancia = distancia;
					Tiempo = MapsGest.GetTiempo();
                    hosp.tiempo = Tiempo;
					ordenlista();
                    BindingContext = this;
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

		public ICommand RefreshCommand
		{
			get
			{
				return new Command(async () =>
				{
					IsRefreshing = true;

					await ObtenerTodoslosHospitales();

					IsRefreshing = false;
				});
			}
		}

    }
}

   