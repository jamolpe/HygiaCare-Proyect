using Hygia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Hygia.DataService
{
	public class ApiDataH
	{
		public int id { get; set; }
		public string ciudad { get; set; }
		public string nombre { get; set; }
		public string comunidadAutonoma { get; set; }
		public string coordenadaX { get; set; }
		public string coordenadaY { get; set; }
		public string URLImagen { get; set; }
	}

    public class HospitalesLista
	{
		public List<ApiDataH> obj = new List<ApiDataH>();
         	static List<Hospital> listaHos = new List<Hospital>();
		public List<Hospital> getlistaHos(){
			return listaHos;
		}

	/*
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
		*/


		public async Task getHospitalesAPI(){
			listaHos = new List<Hospital>();
			var uri = new Uri("http://apihsp.azurewebsites.net/hospitales");
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiDataH>>(content);
				foreach(ApiDataH data in obj){
					Model.Hospital hosp = new Hospital();
					hosp.Nombre = data.nombre;
					hosp.Ciudad = data.ciudad;
					hosp.coordenadaX = Convert.ToDouble(data.coordenadaX);
					hosp.coordenadaY = Convert.ToDouble(data.coordenadaY);
					hosp.ComunidadAutonoma = data.comunidadAutonoma;
					hosp.Ciudad = "";
					hosp.OcupacionHoras = new Dictionary<int, int>();
					listaHos.Add(hosp);

				}
			}

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
