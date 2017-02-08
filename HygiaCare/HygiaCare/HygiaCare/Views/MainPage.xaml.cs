using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HygiaCare.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
        }
   

        public void lvItemTapped(object sender, EventArgs e)
        {
            var myListView = (ListView)sender;
            var myItem =myListView.SelectedItem;
            var hospital = new  Models.Hospital();
            hospital = (Models.Hospital)myItem;

            DisplayAlert("Alert", "You have been alerted" + hospital.Name, "OK");
        }


    }


}
