using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hygia.Model
{
    public class Hospital
    {
        public String Nombre { get; set; }
        public String ComunidadAutonoma { get; set; }
        public String Ciudad { get; set; }
        public String coordenadaX{ get; set; }
	public String coordenadaY { get; set; }
	public String coordenadaZ { get; set; }
        public String Imagen { get; set; }

        public Dictionary<int,int> OcupacionHoras { get; set; }

        
        
    }
}
