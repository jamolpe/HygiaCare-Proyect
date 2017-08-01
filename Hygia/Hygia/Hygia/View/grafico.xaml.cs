using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Hygia.View
{
    public partial class grafico : ContentView
    {
        public grafico()
        {
            InitializeComponent();
        }

        public void graficoBox(List<BoxView> grafos)
        {
            grid.Children.AddHorizontal(grafos);
			Device.StartTimer(TimeSpan.FromMilliseconds(15), OnTimerTick);
        }

        public  TapGestureRecognizer nTap(){
			TapGestureRecognizer tapGesture = new TapGestureRecognizer();
			tapGesture.Tapped += OnBoxViewTapped;
            return tapGesture;
        }
        // Set text to overlay Label and make it visible.
        void OnBoxViewTapped(object sender, EventArgs args)
        {
            BoxView boxView = (BoxView)sender;
            label.Text = String.Format("A las {0} " + "hay una ocupacion de {1} %.", boxView.StyleId, (int)boxView.HeightRequest);
            overlay.Opacity = 1;

        }
        // Decrease visibility of overlay.
        bool OnTimerTick()
        {
            overlay.Opacity = Math.Max(0, overlay.Opacity - 0.0025);
            return true;

        }
    }
}
