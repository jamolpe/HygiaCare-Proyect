using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using System.Net.Http;
using static Hygia.ViewModel.GoogleDirectionsJSONTranslation;

namespace Hygia.ViewModel
{
    public class WorkingMaps
    {
        private Double Lat;
        private Double Long;
        private Double Alt;
        private RootObject obj;
        public WorkingMaps()
        {
            Lat = 0.0;
            Long = 0.0;
            obj = new RootObject();
        }
        public async Task<bool> GetActualPosition()
        {
            bool conseguido = false;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(8000);
                Lat = position.Latitude;
                Long = position.Longitude;
                conseguido = true;
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Imposible recuperar la posicion " + ex);
               
            }

            return conseguido;
            
        }

        public async Task GetJSONDirecciones(Double latitudDest,Double longitudDest,Double altitudDest)
        {
            
            var uri = new Uri("https://maps.googleapis.com/maps/api/directions/json?origin="+Lat.ToString().Replace(",",".")+","+Long.ToString().Replace(",", ".")+"&destination="+latitudDest.ToString().Replace(",", ".")+","+longitudDest.ToString().Replace(",", ".")+"&key=AIzaSyDyRgS5O3z_lwcRXVWXERo7z-j2yK3ESv0");
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(content);
            }

          }

        public String GetDistancia()
        {
            GoogleDirectionsJSONTranslation.Distance distancia;
            if (obj != null)
            {
                Route ruta = obj.routes.First<Route>();
                 distancia = ruta.legs.First<Leg>().distance;
            }else
            {
                return "0";
            }
           

            return distancia.text;
        }

        public String GetTiempo()
        {
            GoogleDirectionsJSONTranslation.Duration duracion;

            if (obj!=null){
                Route ruta = obj.routes.First<Route>();
                duracion = ruta.legs.First<Leg>().duration;
            }else
            {
                return "0";
            }

            return duracion.text;
        }

        public Double GetLong()
        {
            return Long;
        }

        public Double GetLat()
        {
            return Lat;
        }

        public Double GetAlt()
        {
            return Alt;
        }


    }
}
