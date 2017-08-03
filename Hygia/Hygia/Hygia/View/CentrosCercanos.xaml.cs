using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Hygia.Model;
using Hygia.ViewModel;

namespace Hygia.View
{
    public partial class CentrosCercanos : ContentView
    {
        public CentrosCercanos(Hospital hosp,float distanciapadre)
        {
            InitializeComponent();
            setNombre(hosp.Nombre);
            setDistancia(hosp.distancia);
            coloreartiempo(hosp,distanciapadre);
        }

        public void setNombre(string nombre){
            lblnombre.Text = nombre;
        }
        public void setDistancia(string distancia){
            lblDistancia.Text = distancia;
        }
        public void coloreartiempo(Hospital hosp,float distanciapadre){
            var part = hosp.distancia.Split(' ');
            if(float.Parse(part[0]) > distanciapadre){
                recuadro.BackgroundColor = Color.Aqua;
            }else{
                recuadro.BackgroundColor = Color.ForestGreen;
            }
        }
    }
}
