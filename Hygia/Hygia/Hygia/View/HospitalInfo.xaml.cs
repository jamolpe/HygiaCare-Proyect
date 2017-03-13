﻿using Hygia.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Hygia.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HospitalInfo : CarouselPage
    {
        Hospital hospital;
        Position position;
        List<ContentPage> pages = new List<ContentPage>(0);
        public HospitalInfo(Hospital hospital)
        {
            InitializeComponent();
            this.Title = hospital.Nombre;
            this.hospital = hospital;
            this.Titulo.Text = hospital.Nombre;
            AddPin();
            MoveToPing();
            //pages.Add(new HospitalMap());

            //this.Children.Add(pages[0]);
        }

        public void AddPin()
        {
            position = new Position(hospital.coordenadaX, hospital.coordenadaY);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = hospital.Nombre,
                Address = hospital.Ciudad + " , " + hospital.ComunidadAutonoma

            };
           
            MyMap.Pins.Add(pin);
            
           
        }

        public void MoveToPing()
        {
            MyMap.MoveToRegion(new MapSpan(position, 0.02, 0.02));
        }
    }


}
   

