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

        public HospitalInfo(Hospital hospital,List<Hospital> listcercanos)
        {
            InitializeComponent();
            this.Title = hospital.Nombre;
            this.hospital = hospital;
            //this.Titulo.Text = hospital.Nombre;
            this.hospital = hospital;

            AddPin();
            MoveToPing();
            obtenerDatosPantalla();
            cargarCentrosCercanos(listcercanos);
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
                    /*for (int i = 0; i < 10;i++){
                        gridgrafico.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    }
                    for (int j = 0; j <= 24;j++){
                        gridgrafico.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(1,GridUnitType.Auto)});
                    }*/
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
        
            /*
              foreach(OcupacionHoras ocupacion in OcupacionH){
                StackLayout stack = new StackLayout()
                {
                    BackgroundColor = Color.Blue
                };
                stack.HorizontalOptions = LayoutOptions.Center;
                var hora = formatearHoras(ocupacion.Hora);
                for (int i = 0; i <= ocupacion.ocupacion;i++){
                    gridgrafico.Children.Add(stack, hora, i);
                }

            }

            var modelEx = new PlotModel { Title = "Example 1" };
            this.graph.Model = GraficoHoras().Result;
            */
            
        }

        /*
        public async Task<PlotModel> GraficoHoras()
        {
            var plotModel = new PlotModel { Title = "Ocupacion por horas" };


            foreach (OcupacionHoras ocupacion in OcupacionH)
            {
                var s1 = new BarSeries { Title = ocupacion.Hora, StrokeColor = OxyColors.Aqua };
                s1.Items.Add(new BarItem{Value = ocupacion.ocupacion});
				plotModel.Series.Add(s1);
            }
            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
			categoryAxis.Labels.Add("Category A");
            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);
			return plotModel;
        }
        */
        public int formatearHoras(String hora){
            var part = hora.Split(':');
            return Int32.Parse(part[0]);
        }

        public void cargarCentrosCercanos(List<Hospital> lista){
            foreach(Hospital hosp in lista){
                scllcercanos.Children.Add(new CentrosCercanos(hosp));
            }
        }
    }
}
   

