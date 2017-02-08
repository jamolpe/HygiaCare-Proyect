using Hygia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hygia.Services
{
    public class HospitalesServices
    {

        public List<Hospital> GetHospitales()
        {
            var list = new List<Hospital>
            {
                new Hospital
                {
                       Name = "Vinalopo",
                       ComunidadAutonoma = "Comunidad Valenciana",
                },
                new Hospital
                {
                       Name = "Torrevieja",
                       ComunidadAutonoma = "Comunidad Valenciana"
                },
                new Hospital
                {
                       Name = "Alcira",
                       ComunidadAutonoma = "Comunidad Valenciana"
                },
            };
            return list;
        }
    }
}
