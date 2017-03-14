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
        public async Task GetActualPosition()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(1000);
                Lat = position.Latitude;
                Long = position.Longitude;
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Imposible recuperar la posicion " + ex);
               
            }
            
        }

        public async Task GetJSONDirecciones(Double latitudDest,Double longitudDest,Double altitudDest)
        {
            await GetActualPosition();
            var uri = new Uri("https://maps.googleapis.com/maps/api/directions/json?origin="+Lat+","+Long+"&destination="+latitudDest+","+longitudDest+"&key=AIzaSyDyRgS5O3z_lwcRXVWXERo7z-j2yK3ESv0");
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
