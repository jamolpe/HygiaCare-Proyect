using Hygia.Model;
using Hygia.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
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
        List<OcupacionHoras> OcupacionH;

        public HospitalInfo(Hospital hospital,System.Collections.IEnumerable listahospitales)
        {
            InitializeComponent();
            this.Title = hospital.Nombre;
            this.hospital = hospital;
            //this.Titulo.Text = hospital.Nombre;
            this.hospital = hospital;

            AddPin();
            MoveToPing();
            obtenerDatosPantalla();
            if(hospital.distancia != null){
				cargarCentrosCercanos(ObtenerHospCercanos(hospital, listahospitales));
			}
		}

        public void AddPin()
        {
            Double x = Double.Parse(hospital.coordenadaX.Replace(".", ","));
            Double y = Double.Parse(hospital.coordenadaY.Replace(".", ","));
            position = new Position(x, y);
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

        public async void obtenerDatosPantalla()
        {

            if (await Information())
            {
                if (await InformacionOcupacion())
                {
                   
                    trabajarGrafo();
                };
            }
        }
        public async Task<bool> Information()
        {
            MapsGest = new WorkingMaps();

            if (await MapsGest.GetActualPosition())
            {
                await MapsGest.GetJSONDirecciones(hospital.coordenadaX, hospital.coordenadaY, hospital.coordenadaZ);
                distancia = MapsGest.GetDistancia();
                Tiempo = MapsGest.GetTiempo();

            }
            else
            {
                distancia = "N|N";
                Tiempo = "N|N";
            }

            LblDistancia.Text = distancia;
            LblTiempo.Text = Tiempo;
            imgping.IsVisible = true;
            imgreloj.IsVisible = true;
            ACCargandoTiempo.IsRunning = false;
            ACCargandoDistancia.IsRunning = false;

            return true;
        }

        public async Task<bool> InformacionOcupacion()
        {
            info = new HospitalInfoViewModel();

            if (await info.ObtenerOcupacionAPI(hospital.id))
            {
                OcupacionH = info.ObtenerOcupacion();
                reordenarHoras();
                return true;
            }
            return false;

        }
        public void reordenarHoras(){
          
            OcupacionH = (from  oc in OcupacionH
                          where oc.Hora != "00:00"
                orderby oc.Hora ascending
                          select oc).ToList();
            
        }
        public void trabajarGrafo()
        {
            List<BoxView> grafos = new List<BoxView>();
            foreach (OcupacionHoras ocupacion in OcupacionH)
            {
                var color = Color.Accent;
                var tiempo = ocupacion.Hora.Split(':');
                var hoy = DateTime.Now.ToString("HH");
                if ( tiempo[0] == hoy){
                   color = Color.SeaGreen; 
                } ;
                BoxView box = new BoxView()
                {

                    Color = color,


                    HeightRequest = ocupacion.ocupacion,
                    VerticalOptions = LayoutOptions.End,
                    StyleId = ocupacion.Hora
                };
                box.GestureRecognizers.Add(grafico.nTap());

                grafos.Add(box);
            }
            grafico.graficoBox(grafos);
       
            
        }

       
        public int formatearHoras(String hora){
            var part = hora.Split(':');
            return Int32.Parse(part[0]);
        }

        public void cargarCentrosCercanos(List<Hospital> lista){
            foreach(Hospital hosp in lista){
                var part = this.hospital.distancia.Split(' ');
                scllcercanos.Children.Add(new CentrosCercanos(hosp,float.Parse(part[0]),lista));
                var barra = new BoxView
                {
                    HeightRequest = 10,
                    WidthRequest = 1,
                    Color=Color.FromRgb(204, 204, 204),
                    Margin = new Thickness(0,10,0,10)
                };
                scllcercanos.Children.Add(barra);
            }
        }

        public List<Hospital> ObtenerHospCercanos(Hospital hosp,System.Collections.IEnumerable listaInicial)
		{
			int i = 0;
			int posicion = 0;
			int inicial = 0;
			int final = 0;
			List<Hospital> listaactual = new List<Hospital>();
			List<Hospital> lista = new List<Hospital>();
			foreach (Hospital hospital in listaInicial)
			{
				if (hospital.id == hosp.id)
				{
					posicion = i;
				}
				listaactual.Add(hospital);
				i++;
			}

			if (posicion <= 3)
			{
				inicial = 0;
			}
			else
			{
				inicial = posicion - 3;
			}

			if (i > posicion + 3)
			{
				final = posicion + 3;
			}
			else
			{
                final = i - 1;
			}

			for (int j = inicial; j <= final; j++)
			{
				if (listaactual[j].id != hosp.id)
				{
					lista.Add(listaactual[j]);
				}
			}
			return lista;

		}
    }
}
   

