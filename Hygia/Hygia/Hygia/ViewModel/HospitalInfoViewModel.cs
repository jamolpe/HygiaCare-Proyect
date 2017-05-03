using Hygia.DataService;
using Hygia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;namespace Hygia.ViewModel
{
    public class HospitalInfoViewModel
    {
        
        public Dictionary<string,int> OcupacionHoras;
        public HospitalesLista Hospitales;

        public HospitalInfoViewModel(){
            Hospitales = new HospitalesLista();
        }
        public async Task<bool> ObtenerOcupacionAPI(int id){

            await Hospitales.ObtenerHorasAPI(id);
        
            OcupacionHoras = Hospitales.getOcupacionHoras();
            return true;

        }

        public Dictionary<string,int> ObtenerOcupacion(){
            return OcupacionHoras;
        }


    }
}
