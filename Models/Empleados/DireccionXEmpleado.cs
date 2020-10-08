using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class DireccionXEmpleado
    {
        public string cP { get; set; }
        public string calleNumero { get; set; }
        public int codColonia { get; set; }
        public int codEstado { get; set; }
        public int codMunicipio { get; set; }
        //public int codigo { get; set; }
        //public int codEmpleado { get; set; }
        public string colonia { get; set; }
        public string estado { get; set; }
        public string municipio { get; set; }
    }
}
