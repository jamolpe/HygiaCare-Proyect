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
                    coordenadaX=38.2552265,
                    coordenadaY = -0.7168859,
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
                    Nombre = "Hospital de Alzira",
                    Ciudad = "Alcira",
                    ComunidadAutonoma = "Comunidad Valenciana",
                    coordenadaX=39.1597552,
                    coordenadaY = -0.4173544,
                    Imagen="LogoRivera.png"
                },
                new Model.Hospital
                {
                    Nombre = "Hospital La Fe",
                    Ciudad = "Valencia",
                    ComunidadAutonoma = "Comunidad Valenciana",
                    coordenadaX=39.4431804,
                    coordenadaY = -0.3768042
                },
            };

            return list;
        }
    }
}
