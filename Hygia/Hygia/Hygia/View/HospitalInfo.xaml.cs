using Hygia.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hygia.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HospitalInfo : CarouselPage
    {
        public HospitalInfo(Hospital hospital)
        {
            InitializeComponent();
            this.Title = hospital.Nombre;

            this.Titulo.Text = hospital.Nombre;
         
        }
    }

   
}
