using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Hygia.Views
{
    public partial class ListaHospitalesV : ContentPage
    {
        public ListaHospitalesV()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
         
            //Mymap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(36.89, 10.18),Distance.FromKilometers(5)));
        }

        public void lvItemTapped(object sender, EventArgs e)
        {
          
            var myCell = (ViewCell)sender;

            if (myCell.Height == 500)
            {
                myCell.Height = 50;
                var item = (Models.Hospital)myCell.BindingContext;
                myCell.View = new StackLayout
                    {
                        Children =
                        {
                             new Label
                            {
                                Text = item.Name,
                                FontSize=25
                            },
                            new Label
                            {
                                Text=item.ComunidadAutonoma,
                                FontSize=15
                            }
                        }
                    
                };
                myCell.ForceUpdateSize();
            }else
            {
                myCell.Height = 500;
                var item = (Models.Hospital)myCell.BindingContext;
                myCell.View = new StackLayout
                    {
                        Children = {
                            new Label
                            {
                                Text = item.Name,
                                FontSize=25
                            },
                            new Label
                            {
                                Text=item.ComunidadAutonoma,
                                FontSize=15
                            },
                            new StackLayout
                            {
                                VerticalOptions=LayoutOptions.FillAndExpand,
                                Orientation=StackOrientation.Vertical,
                                Children =
                                {
                                    new Map
                                    {
                                        IsShowingUser=true,
                                        MapType=MapType.Hybrid
                                    }
                                }

                            }

                        }
                        
                    
                };
                myCell.ForceUpdateSize();
  
            }
            
            /*
            var myListView = (ListView)sender;
            var myItem = myListView.SelectedItem;
            var hospital = new Models.Hospital();
            hospital = (Models.Hospital)myItem;

            DisplayAlert("Alert", "You have been alerted" + hospital.Name, "OK");
            */
        }
        

    }
}
