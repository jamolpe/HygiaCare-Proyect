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
        static List<Hospital> listaHos = new List<Hospital>();
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
                    coordenadaZ=17,
                    Imagen = "LogoVinalopo.png",
                    OcupacionHoras = new Dictionary<int, int>()

                },
                new Model.Hospital
                {
                    Nombre = "Hospital de Torrevieja",
                    Ciudad = "Torrevieja",
                    ComunidadAutonoma = "Comunidad Valenciana",
                    coordenadaX=37.9636491,
                    coordenadaY = -0.7162335,
                    coordenadaZ=18,
                    Imagen="LogoTorrevieja.png",
                    OcupacionHoras = new Dictionary<int, int>()
                },

                new Model.Hospital
                {
                    Nombre = "Hospital de Alzira",
                    Ciudad = "Alzira",
                    ComunidadAutonoma = "Comunidad Valenciana",
                    coordenadaX=39.1597552,
                    coordenadaY = -0.4173544,
                    coordenadaZ=18.25,
                    Imagen="LogoRivera.png",
                     OcupacionHoras = new Dictionary<int, int>()
                },
                new Model.Hospital
                {
                    Nombre = "Hospital La Fe",
                    Ciudad = "Valencia",
                    ComunidadAutonoma = "Comunidad Valenciana",
                    coordenadaX=39.4431804,
                    coordenadaY = -0.3768042,
                    coordenadaZ=18,
                     OcupacionHoras = new Dictionary<int, int>()
                },
            };

            listaHos = list;
            return listaHos;
        }

        public static Dictionary<int,int> ObtenerHoras(String Hospital)
        {
            Dictionary<int, int> Hosphours = new Dictionary<int, int>();
            foreach(Hospital obj in listaHos)
            {
                for(int i = 1; i <= 24; i++)
                {
                    Random random = new Random();
                    obj.OcupacionHoras.Add(i, random.Next() * (1000 - 0) + 0);
                }
                if (obj.Nombre == Hospital)
                {
                    Hosphours = obj.OcupacionHoras;
                }
            }

            return Hosphours;
        }
    }
}
