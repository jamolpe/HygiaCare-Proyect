using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HygiaCare.Pages
{
    public partial class InicioXAML : ContentPage
    {
        public InicioXAML()
        {
            InitializeComponent();
        }

        void OnButtonClicked(object sender,EventArgs args)
        {
            DisplayAlert("Clickeado","Me has clickeado","Salir");
        }
    }
}
