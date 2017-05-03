using Hygia.Model;
using Hygia.ViewModel;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using static Hygia.ViewModel.GoogleDirectionsJSONTranslation;

namespace Hygia.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HospitalInfo : CarouselPage
    {
        Hospital hospital;
        Position position;
        List<ContentPage> pages = new List<ContentPage>(0);
        String distancia;
        String Tiempo;
        WorkingMaps MapsGest;
        HospitalInfoViewModel info;
        Dictionary<string,int> OcupacionH;

        public HospitalInfo(Hospital hospital)
        {
            InitializeComponent();
            this.Title = hospital.Nombre;
            this.hospital = hospital;
            //this.Titulo.Text = hospital.Nombre;
            this.hospital = hospital;
            
            AddPin();
            MoveToPing();
            Information();
            InformacionOcupacion();

        }

        public void AddPin()
        {
		Double x = Double.Parse(hospital.coordenadaX.Replace(".",","));
		Double y = Double.Parse(hospital.coordenadaY.Replace(".",","));
		position = new Position(x,y);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = hospital.Nombre,
                Address = hospital.Ciudad + " , " + hospital.ComunidadAutonoma
            };
            MyMap.Pins.Add(pin);
        }
        public void MoveToPing()
        {
            MyMap.MoveToRegion(new MapSpan(position, 0.02, 0.02));
        }

        public async void Information()
        {
            MapsGest = new WorkingMaps();

            if(await MapsGest.GetActualPosition())
            {
                await MapsGest.GetJSONDirecciones(hospital.coordenadaX, hospital.coordenadaY, hospital.coordenadaZ);
                distancia = MapsGest.GetDistancia();
                Tiempo = MapsGest.GetTiempo();
            }else
            {
                distancia = "N|N";
                Tiempo = "N|N";
            }
            
            LblDistancia.Text = distancia;
            LblTiempo.Text = Tiempo;
            ACCargandoTiempo.IsRunning = false;
            ACCargandoDistancia.IsRunning = false;
        }

        public async void InformacionOcupacion(){
            info = new HospitalInfoViewModel();

            if(await info.ObtenerOcupacionAPI(hospital.id)){
                OcupacionH = info.ObtenerOcupacion();
            }
        }



    }
}
   

