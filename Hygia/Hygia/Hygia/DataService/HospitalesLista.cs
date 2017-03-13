using Hygia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hygia.DataService
{
    public class HospitalesLista
    {
        public List<Hospital> GetHospitales()
        {
            var list = new List<Hospital>
            {
                new Model.Hospital
                {
                    Nombre = "Hospital de Vinalopo",
                    Ciudad = "Elche",
                    ComunidadAutonoma = "Comunidad Valenciana",
                    coordenadaX=0.12425F,
                    coordenadaY = 0.21425F,
                    Imagen = "LogoVinalopo.png"
                    

                },
                new Model.Hospital
                {
                    Nombre = "Hospital de Torrevieja",
                    Ciudad = "Torrevieja",
                    ComunidadAutonoma = "Comunidad Valenciana",
                    coordenadaX=37.9636491,
                    coordenadaY = -0.7162335,
                    Imagen="LogoTorrevieja.png"
                },

                new Model.Hospital
                {
                    Nombre = "Hospital de Alcira",
                    Ciudad = "Alcira",
                    ComunidadAutonoma = "Comunidad Valenciana",
                    coordenadaX=12.12425F,
                    coordenadaY = 24.21425F,
                    Imagen="LogoRivera.png"
                },
                new Model.Hospital
                {
                    Nombre = "Hospital de Valencia",
                    Ciudad = "Valencia",
                    ComunidadAutonoma = "Comunidad Valenciana",
                    coordenadaX=12.12425F,
                    coordenadaY = 24.21425F
                },
            };

            return list;
        }
    }
}
