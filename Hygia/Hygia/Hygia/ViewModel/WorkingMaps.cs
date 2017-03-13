using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Hygia.ViewModel
{
    public class WorkingMaps
    {
        Double Lat;
        Double Long;

        WorkingMaps()
        {
            Lat = 0.0;
            Long = 0.0;
        }
        public async void GetActualPosition()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                Lat = position.Latitude;
                Long = position.Longitude;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Imposible recuperar la posicion " + ex);
               
            }
            
        }
    }
}
