﻿using Hygia.Model;
using Hygia.ViewModel;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using static Hygia.ViewModel.GoogleDirectionsJSONTranslation;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

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

        public HospitalInfo(Hospital hospital)
        {
            InitializeComponent();
            this.Title = hospital.Nombre;
            this.hospital = hospital;
            //this.Titulo.Text = hospital.Nombre;
            this.hospital = hospital;

            AddPin();
            MoveToPing();
            obtenerDatosPantalla();

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
            /*
            LblDistancia.Text = distancia;
            LblTiempo.Text = Tiempo;
            ACCargandoTiempo.IsRunning = false;
            ACCargandoDistancia.IsRunning = false;
            */
            return true;
        }

        public async Task<bool> InformacionOcupacion()
        {
            info = new HospitalInfoViewModel();

            if (await info.ObtenerOcupacionAPI(hospital.id))
            {
                OcupacionH = info.ObtenerOcupacion();
                return true;
            }
            return false;

        }

        public void trabajarGrafo()
        {
            var modelEx = new PlotModel { Title = "Example 1" };
            this.graph.Model = GraficoHoras().Result;
        }
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

        public int formatearHoras(String hora){
            var part = hora.Split(':');
            return Int32.Parse(part[0]);
        }
    }
}
   

