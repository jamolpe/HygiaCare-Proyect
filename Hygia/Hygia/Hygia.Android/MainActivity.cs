using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Locations;
using Xamarin.Forms;

namespace Hygia.Droid
{
    [Activity(Label = "Hygia", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            global::Xamarin.FormsMaps.Init(this, bundle);
            
            LoadApplication(new App());
            LocationOn();
        }

        public void LocationOn()
        {
            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
            //set alert for executing the task
      
            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Por favor activa el posicionamiento GPS");
                alert.SetPositiveButton("Activar", (senderAlert, args) => {
                    Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                    Forms.Context.StartActivity(gpsSettingIntent);
                });
                alert.SetNegativeButton("No", (senderAlert, args) => {
                    //perform your own task for this conditional button click
                });
                RunOnUiThread(() => {
                    alert.Show();
                });

            }
        }
    }
}

