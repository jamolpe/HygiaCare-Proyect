using Hygia.Model;
using Hygia.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ListaHospitalesInicio : ContentPage
    {

        ObservableCollection<Hospital> ListaHospitales = new ObservableCollection<Hospital>();
        public ListaHospitalesInicio()
        {
            InitializeComponent();
            var HospitalesList = new HospitalesViewModel();
            ListaHospitales = new ObservableCollection<Hospital>(HospitalesList.ListaHospitales);
            ListHospitales.ItemsSource = ListaHospitales;
        }
    }

    class ListaHospitalesInicioViewModel : INotifyPropertyChanged
    {

        public ListaHospitalesInicioViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
