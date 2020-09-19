using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class DireccionXEmpleado
    {
        public int Codigo { get; set; }
        public int CodEmpleado { get; set; }
        public string CP { get; set; }
        public int CodColonia { get; set; }
        public int CodMunicipio { get; set; }
        public int CodEstado { get; set; }
        public string CalleNumero { get; set; }
    }
}
