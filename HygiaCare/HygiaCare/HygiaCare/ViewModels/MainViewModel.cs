using HygiaCare.Models;
using HygiaCare.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HygiaCare.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private List<Hospital> _HospitalesList;

        public List<Hospital> HospitalesList {
            get { return _HospitalesList; }

            set {
                _HospitalesList = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            var hospitalesServices = new HospitalesServices();

            HospitalesList = hospitalesServices.GetHospitales();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
