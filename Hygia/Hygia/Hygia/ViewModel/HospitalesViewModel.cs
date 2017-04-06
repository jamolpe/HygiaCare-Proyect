using Hygia.DataService;
using Hygia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hygia.ViewModel
{
    public class HospitalesViewModel
    {
        public List<Hospital> ListaHospitales;

        public HospitalesViewModel()
        {
            var Hospitales = new HospitalesLista();
            ListaHospitales = Hospitales.GetHospitales();
        }

        public Dictionary<int,int> ObtHoras(String nombreHospital)
        {
            return HospitalesLista.ObtenerHoras(nombreHospital);
        }
    }
}
