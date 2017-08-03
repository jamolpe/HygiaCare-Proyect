using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Hygia.Model;
using Hygia.ViewModel;

namespace Hygia.View
{
    public partial class CentrosCercanos : ContentView
    {
        public CentrosCercanos(Hospital hosp)
        {
            InitializeComponent();
            setNombre(hosp.Nombre);
            setDistancia(hosp.distancia);
            coloreartiempo(hosp);
        }

        public void setNombre(string nombre){
            lblnombre.Text = nombre;
        }
        public void setDistancia(string distancia){
            lblDistancia.Text = distancia;
        }
        public void coloreartiempo(Hospital hosp){
            if(hosp.distancia == "15"){
                recuadro.BackgroundColor = Color.Aqua;
            }else{
                recuadro.BackgroundColor = Color.ForestGreen;
            }
        }
    }
}
