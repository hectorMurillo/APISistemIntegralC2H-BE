using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class DireccionXEmpleado
    {
        public string CP { get; set; }
        public string CalleNumero { get; set; }
        public int CodColonia { get; set; }
        public int CodEstado { get; set; }
        public int CodMunicipio { get; set; }
        //public int codigo { get; set; }
        //public int codEmpleado { get; set; }
        public string Colonia { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
    }
}
