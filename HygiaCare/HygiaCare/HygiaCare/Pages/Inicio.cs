using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HygiaCare.Pages
{
    public class Inicio : ContentPage
    {
        public Inicio()
        {
            
            var lbl1 = new Label
            {
                Text = "Prueba de que entro",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };


 
        }
    }
}
