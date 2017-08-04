﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Hygia.Model;
using Hygia.ViewModel;

namespace Hygia.View
{
    public partial class CentrosCercanos : ContentView
    {
        public System.Collections.IEnumerable listaHospitales;
        public Hospital referencia;
        public CentrosCercanos(Hospital hosp,float distanciapadre,System.Collections.IEnumerable lista)
        {
            InitializeComponent();
            listaHospitales = lista;
            setNombre(hosp.Nombre);
            referencia = hosp;
            setDistancia(hosp.distancia);
            coloreartiempo(hosp,distanciapadre);
            recuadro.GestureRecognizers.Add(nTap());
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
		public TapGestureRecognizer nTap()
		{
			TapGestureRecognizer tapGesture = new TapGestureRecognizer();
			tapGesture.Tapped += OnFrameTapped;
			return tapGesture;
		}
		// Set text to overlay Label and make it visible.
		void OnFrameTapped(object sender, EventArgs args)
		{
			Frame frame = (Frame)sender;
            Navigation.PushAsync(new HospitalInfo(referencia, listaHospitales));
		}
    }
}
