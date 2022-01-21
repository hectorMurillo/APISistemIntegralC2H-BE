using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class OperadorEquipo
    {
        public int CodEmpleado { get; set; }
        public string Equipo { get; set; }
        public string Nombre { get; set; }
        public bool Bombeable { get; set; }
        public int CodAyudante { get; set; }
        public string AyudanteNombre { get; set; }
    }
}
