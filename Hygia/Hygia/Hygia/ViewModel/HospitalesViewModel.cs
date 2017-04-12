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

	public HospitalesLista Hospitales;
        public HospitalesViewModel()
        {
            Hospitales = new HospitalesLista();
		
        }

        public Dictionary<int,int> ObtHoras(String nombreHospital)
        {
            return HospitalesLista.ObtenerHoras(nombreHospital);
        }

	public  async Task<bool> ObtenerHospitales(){
		 await Hospitales.getHospitalesAPI();
		
		ListaHospitales = Hospitales.getlistaHos();
			return true;

	}
    }
}
